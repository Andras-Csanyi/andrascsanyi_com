namespace Exercises.Logic.Repository.Chapter;

using Common;
using Models;
using static Prelude;

public partial class ChapterRepository
{
    public Either<ExerciseError, Option<ChapterEntity>> FindByBookIdAndReference(
        long bookId,
        string reference,
        ExercisesContext ctx
    )
    {
        try
        {
            ChapterEntity? hit = ctx.Chapters
                .Where(c => c.BookId == bookId)
                .FirstOrDefault(c => c.Reference == reference);
            return hit == null ? None : Some(hit);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError(
                    $"Error happened while requesting {nameof(ChapterEntity)} " +
                    $"and {nameof(BookEntity)}.{nameof(BookEntity.Id)}: {bookId} " +
                    $"and {nameof(ChapterEntity)}.{nameof(ChapterEntity.Reference)}: {reference}"
                )
            );
        }
    }

    public Either<ExerciseError, Option<ChapterEntity>> FindByReference(
        string reference,
        ExercisesContext ctx
    )
    {
        try
        {
            ChapterEntity? hit = ctx.Chapters.FirstOrDefault(c => c.Reference == reference);
            return hit == null ? None : Some(hit);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError(
                    $"Error happened while requesting {nameof(ChapterEntity)} " +
                    $"and {nameof(ChapterEntity)}.{nameof(ChapterEntity.Reference)}: {reference}"
                )
            );
        }
    }
}