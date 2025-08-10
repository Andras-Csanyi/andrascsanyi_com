namespace Exercises.Logic.Repository.Chapter;

using Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Models;
using static LanguageExt.Prelude;

public partial class ChapterRepository
{
    public Either<ExerciseError, ChapterEntity> Update(ChapterEntity entity,
        ExercisesContext ctx)
    {
        try
        {
            ChapterEntity target = ctx.Chapters
                .First(id => id.Id == entity.Id);
            target.Title = entity.Title;
            target.BookId = entity.BookId;
            target.PageEnd = entity.PageEnd;
            target.PageStart = entity.PageStart;
            target.Reference = entity.Reference;
            ctx.Entry(target).State = EntityState.Modified;
            ctx.SaveChanges();
            return Right(target);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError($"Error happened while updating {nameof(ChapterEntity)} with id: {entity.Id}."));
        }
    }
}