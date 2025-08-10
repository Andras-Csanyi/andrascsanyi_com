namespace Exercises.Logic.Scenarios.Section.Add;

using CatalogParser.Model;
using Common;
using LanguageExt;
using Repository;
using Repository.Models;
using Repository.Section;
using static LanguageExt.Prelude;

public class AddNewSectionScenario(
    AddNewSectionScenarioInputValidation validator,
    SectionRepository sectionRepository
)
{
    public Either<ExerciseError, SectionEntity> Execute(
        Section parsedSection,
        long chapterId,
        ExercisesContext ctx
    ) =>
        from mappedInput in MapInputToEntity(parsedSection, chapterId)
        from validatedInput in ValidateInput(mappedInput)
        from newEntity in SaveEntity(validatedInput, ctx)
        select newEntity;

    private Either<ExerciseError, SectionEntity> SaveEntity(
        SectionEntity entity,
        ExercisesContext ctx
    ) =>
        sectionRepository.Add(entity, ctx);

    private Either<ExerciseError, SectionEntity> ValidateInput(
        SectionEntity mappedInput
    ) =>
        validator.IsValid(mappedInput);

    private Either<ExerciseError, SectionEntity> MapInputToEntity(
        Section parsedSection,
        long chapterId
    ) =>
        parsedSection.ToEntity().Match(
            Right: result =>
            {
                result.ChapterId = chapterId;
                return Right<ExerciseError, SectionEntity>(result);
            },
            Left: ex => Left(
                new ExerciseError($"Error happened while mapping {nameof(ChapterEntity)}. Error: {ex.Message}")));
}