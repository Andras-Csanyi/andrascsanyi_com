namespace Exercises.Logic.Scenarios.Generate;

using System.Collections.Immutable;
using CatalogParser;
using Common;
using Generators.LaTeX;
using Repository.Exercise;
using Repository.Models;
using Sync;

public class GenerateFromBooksScenario(
    SyncFsWithDb syncFsWithDb,
    ExerciseRepository exerciseRepository
)
{
    private readonly Random _random = new();

    public int Execute(GenerateFromBooksScenarioParameters parameters)
    {
        Either<ExerciseError, Unit> scenarioResult = from exerciseRecord in CatalogParser.Parse()
            from syncResult in syncFsWithDb.Execute(exerciseRecord)
            from selectedExercises in SelectExercises(parameters)
            from enrichedExercises in EnrichSelectedExercises(selectedExercises)
                .Do((res) =>
                    {
                        res.ForEach(item =>
                            {
                                Console.WriteLine(
                                    $"id: {item.Id} " +
                                    $"topic id: {item.TopicId} " +
                                    $"topic name: {item.Topic.Name} "
                                );
                            }
                        );
                    }
                )
            from createdFiles in CreateLatexFile(enrichedExercises)
            select Unit.Default;

        return scenarioResult.Match(
            Right: yolo =>
            {
                Console.WriteLine("Executed successfully");
                return 1;
            },
            Left: nopes =>
            {
                Console.WriteLine($"Errors: {nopes.Message}");
                return 0;
            }
        );
    }

    private Either<ExerciseError, Unit> CreateLatexFile(ImmutableList<ExerciseEntity> exercises) =>
        LaTeXGenerator.Execute(exercises);

    private Either<ExerciseError, ImmutableList<ExerciseEntity>> EnrichSelectedExercises(
        ImmutableList<ExerciseEntity> selectedExercises
    )
    {
        ImmutableList<long> exerciseIds = selectedExercises.Select(item => item.Id)
            .ToImmutableList();
        return from enrichedList in exerciseRepository.EnrichExercises(exerciseIds)
            select enrichedList;
    }

    private Either<ExerciseError, ImmutableList<ExerciseEntity>> SelectExercises(
        GenerateFromBooksScenarioParameters parameters
    ) =>
        from booksListInParam in ExtractBooksFromParamForQuery(parameters.Books)
        from exercisesAcrossBooks in GetExercisesAcrossBooks(booksListInParam)
        from selectedApplicationExercises in SelectApplicationExercises(exercisesAcrossBooks, parameters)
        from skillExercisesAddedList in SelectAndAppendSkillExercises(
            exercisesAcrossBooks,
            selectedApplicationExercises,
            parameters
        )
        from selectedConceptsAdded in SelectAndAppendConceptExercises(
            exercisesAcrossBooks,
            skillExercisesAddedList,
            parameters
        )
        from selectedDiscussionsAdded in SelectAndAppendDiscussionExercises(
            exercisesAcrossBooks,
            selectedConceptsAdded,
            parameters
        )
        select selectedDiscussionsAdded;

    private Either<ExerciseError, ImmutableList<ExerciseEntity>> SelectAndAppendDiscussionExercises(
        List<ExerciseEntity> exercisesAcrossBooks,
        ImmutableList<ExerciseEntity> selectedConceptsAdded,
        GenerateFromBooksScenarioParameters parameters
    )
    {
        List<ExerciseEntity> discussions = exercisesAcrossBooks.Where(w => w.ExerciseType == ExerciseType.Discussion)
            .OrderBy(_ => _random.Next())
            .Take(parameters.DiscussionQuestionVolume)
            .ToList();
        ImmutableList<ExerciseEntity> result = selectedConceptsAdded.AddRange(discussions);
        return Right(result);
    }

    private Either<ExerciseError, ImmutableList<ExerciseEntity>> SelectAndAppendConceptExercises(
        List<ExerciseEntity> exerciseAcrossBooks,
        ImmutableList<ExerciseEntity> selectedApplicationExercises,
        GenerateFromBooksScenarioParameters parameters
    )
    {
        List<ExerciseEntity> conceptExercises = exerciseAcrossBooks.Where(w => w.ExerciseType == ExerciseType.Concept)
            .OrderBy(_ => _random.Next())
            .Take(parameters.ConceptQuestionVolume)
            .ToList();
        ImmutableList<ExerciseEntity> result = selectedApplicationExercises.AddRange(conceptExercises);
        return Right(result);
    }

    private Either<ExerciseError, ImmutableList<ExerciseEntity>> SelectAndAppendSkillExercises(
        List<ExerciseEntity> exercisesAcrossBooks,
        ImmutableList<ExerciseEntity> toAppendTo,
        GenerateFromBooksScenarioParameters parameters
    )
    {
        List<ExerciseEntity> skillExercises = exercisesAcrossBooks.Where(w => w.ExerciseType == ExerciseType.Skill)
            .OrderBy(_ => _random.Next())
            .Take(parameters.SkillQuestionVolume)
            .ToList();
        ImmutableList<ExerciseEntity> appended = toAppendTo.AddRange(skillExercises);
        return Right(appended);
    }

    private Either<ExerciseError, ImmutableList<ExerciseEntity>> SelectApplicationExercises(
        List<ExerciseEntity> exercisesAcrossBooks,
        GenerateFromBooksScenarioParameters parameters
    )
    {
        ImmutableList<ExerciseEntity> result = exercisesAcrossBooks
            .Where(w => w.ExerciseType == ExerciseType.Application)
            .OrderBy(_ => _random.Next())
            .Take(parameters.ApplicationQuestionVolume)
            .ToImmutableList();
        return Right(result);
    }

    private Either<ExerciseError, List<ExerciseEntity>> GetExercisesAcrossBooks(string[] booksListInParam) =>
        from exercises in exerciseRepository.FindByBookReferences(booksListInParam)
        select exercises;

    private static Either<ExerciseError, string[]> ExtractBooksFromParamForQuery(
        string booksParameter
    )
    {
        try
        {
            string[] books = booksParameter.Split(",");
            return Right(books);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}