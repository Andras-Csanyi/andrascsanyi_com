namespace Exercises.Logic.Repository.Topic;

using Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Models;
using static LanguageExt.Prelude;

public partial class TopicRepository
{
    public Either<ExerciseError, List<TopicEntity>> GetAll(
        ExercisesContext ctx
    )
    {
        try
        {
            List<TopicEntity> result = ctx.Topics
                .Include(topic => topic.Books)
                .ThenInclude(book => book.Chapters)
                .ThenInclude(chapter => chapter.Sections)
                .ThenInclude(section => section.Exercises)
                .ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}