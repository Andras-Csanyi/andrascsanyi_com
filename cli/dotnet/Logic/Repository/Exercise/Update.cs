namespace Exercises.Logic.Repository.Exercise;

using Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Models;

public partial class ExerciseRepository
{
    public Either<ExerciseError, ExerciseEntity> Update(
        long targetId,
        ExerciseEntity updateData,
        ExercisesContext ctx
    )
    {
        try
        {
            ExerciseEntity target = ctx.Exercises
                .First(e => e.Id == targetId);
            target.IdInTheBook = updateData.IdInTheBook;
            target.ExerciseType = updateData.ExerciseType;
            target.TopicId = updateData.TopicId;
            target.BookId = updateData.BookId;
            target.ChapterId = updateData.ChapterId;
            target.ChapterIdInTheBook = updateData.ChapterIdInTheBook;
            target.SectionId = updateData.SectionId;
            target.SectionIdInThebook = updateData.SectionIdInThebook;
            ctx.Entry(target).State = EntityState.Modified;
            ctx.SaveChanges();
            return Right(target);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}