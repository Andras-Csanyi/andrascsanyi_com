namespace Exercises.Logic.Repository.Exercise;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class ExerciseRepository
{
    public Either<ExerciseError, ExerciseEntity> Find(
        ExerciseEntity input,
        ExercisesContext ctx
    )
    {
        try
        {
            ExerciseEntity? target = ctx.Exercises
                .Where(e => e.TopicId == input.TopicId)
                .Where(e => e.BookId == input.BookId)
                .Where(e => e.ChapterId == input.ChapterId)
                .Where(e => e.ChapterIdInTheBook == input.ChapterIdInTheBook)
                .Where(e => e.SectionId == input.SectionId)
                .Where(e => e.SectionIdInThebook == input.SectionIdInThebook)
                .First(e => e.IdInTheBook == input.IdInTheBook);
            return Right(target);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}