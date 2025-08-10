namespace Exercises.Logic.Scenarios.Chapter.Update;

using CatalogParser.Model;
using Common;
using LanguageExt;
using Repository;
using Repository.Chapter;
using Repository.Models;
using static LanguageExt.Prelude;

public class UpdateChapterScenario(
    UpdateChapterScenarioInputValidator validator,
    ChapterRepository chapterRepository
)
{
    public Either<ExerciseError, ChapterEntity> Execute(Chapter parsedChapter,
        long bookId,
        ExercisesContext ctx) =>
        from mappedInput in MapInputToEntity(parsedChapter, bookId)
        from validatedInput in ValidateInput(mappedInput)
        from updatedEntity in UpdateEntity(validatedInput, ctx)
        select updatedEntity;

    private Either<ExerciseError, ChapterEntity> UpdateEntity(ChapterEntity entity,
        ExercisesContext ctx) =>
        chapterRepository.Update(entity, ctx);

    private Either<ExerciseError, ChapterEntity> ValidateInput(ChapterEntity input) =>
        validator.IsValid(input);

    private Either<ExerciseError, ChapterEntity> MapInputToEntity(
        Chapter parsedChapter,
        long bookId) => parsedChapter.ToChapterEntity().Match(
        Right: res =>
        {
            res.BookId = bookId;
            return Right<ExerciseError, ChapterEntity>(res);
        },
        Left:
        ex => Left(new ExerciseError($"Chapter update error: {ex.Message}")));
}