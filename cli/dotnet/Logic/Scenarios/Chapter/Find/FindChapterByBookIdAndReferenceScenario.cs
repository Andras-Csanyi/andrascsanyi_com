namespace Exercises.Logic.Scenarios.Chapter.Find;

using Common;
using LanguageExt;
using Repository;
using Repository.Chapter;
using Repository.Models;

public class FindChapterByBookIdAndReferenceScenario(
    ChapterRepository chapterRepository
)
{
    public Either<ExerciseError, ChapterEntity> Execute(long bookId,
        string reference,
        ExercisesContext ctx) =>
        from chapterEntity in chapterRepository.FindByBookIdAndReference(bookId, reference, ctx)
        select chapterEntity;
}