namespace Exercises.Logic.Repository.Chapter;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class ChapterRepository
{
    public Either<ExerciseError, ChapterEntity> FindByBookIdAndReference(
        long bookId,
        string reference,
        ExercisesContext ctx)
    {
        try
        {
            ChapterEntity hit = ctx.Chapters
                .Where(c => c.BookId == bookId)
                .First(c => c.Reference == reference);
            return Right<ExerciseError, ChapterEntity>(hit);
        }
        catch (Exception e)
        {
            return Left<ExerciseError, ChapterEntity>(new ExerciseError(
                $"Error happened while requesting {nameof(ChapterEntity)} " +
                $"and {nameof(BookEntity)}.{nameof(BookEntity.Id)}: {bookId} " +
                $"and {nameof(ChapterEntity)}.{nameof(ChapterEntity.Reference)}: {reference}"));
        }
    }
}