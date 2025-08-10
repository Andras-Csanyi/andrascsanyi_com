namespace Exercises.Logic.Scenarios.Chapter.Add;

using CatalogParser.Model;
using Common;
using LanguageExt;
using Repository;
using Repository.Chapter;
using Repository.Models;
using static LanguageExt.Prelude;

public class AddNewChapterScenario(
    AddNewChapterScenarioInputValidator validator,
    ChapterRepository chapterRepository
)
{
    public Either<ExerciseError, ChapterEntity> Execute(Chapter parsedChapter,
        long bookId,
        ExercisesContext ctx) =>
        from mappedInput in MapInput(parsedChapter, bookId)
        from validatedInput in ValidateInput(mappedInput)
        from newChapterEntity in Save(validatedInput, ctx)
        select newChapterEntity;

    private Either<ExerciseError, ChapterEntity> Save(ChapterEntity input,
        ExercisesContext ctx) =>
        chapterRepository.Add(input, ctx);

    private Either<ExerciseError, ChapterEntity> ValidateInput(ChapterEntity parsedChapter) =>
        validator.IsValid(parsedChapter);

    private Either<ExerciseError, ChapterEntity> MapInput(Chapter parsedChapter,
        long bookId) => parsedChapter.ToChapterEntity().Match(
        Right: result =>
        {
            result.BookId = bookId;
            return Right<ExerciseError, ChapterEntity>(result);
        },
        Left: error => error
    );
}