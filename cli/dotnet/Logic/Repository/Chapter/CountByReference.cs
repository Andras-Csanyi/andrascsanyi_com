namespace Exercises.Logic.Repository.Chapter;

using Common;
using Models;
using static Prelude;

public partial class ChapterRepository
{
    public Either<ExerciseError, int> CountByReference(
        string reference,
        ExercisesContext ctx
    )
    {
        try
        {
            int hit = ctx.Chapters.Count(c => c.Reference == reference);
            return Right(hit);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError(
                    $"Error happened while requesting {nameof(ChapterEntity)} " +
                    $"and {nameof(ChapterEntity)}.{nameof(ChapterEntity.Reference)}: {reference}. " +
                    $"error:  {e.Message}"
                )
            );
        }
    }
}