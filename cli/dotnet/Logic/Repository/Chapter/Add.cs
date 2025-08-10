namespace Exercises.Logic.Repository.Chapter;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class ChapterRepository
{
    public Either<ExerciseError, ChapterEntity> Add(ChapterEntity chapter,
        ExercisesContext ctx)
    {
        try
        {
            ctx.Chapters.Add(chapter);
            ctx.SaveChanges();
            return Right(chapter);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError($"Error happened while recording {nameof(ChapterEntity)}. Error: {e.Message}"));
        }
    }
}