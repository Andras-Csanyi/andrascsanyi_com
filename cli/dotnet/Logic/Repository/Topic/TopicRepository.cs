namespace Exercises.Logic.Repository.Topic;

using Common;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using Models;
using static LanguageExt.Prelude;

public partial class TopicRepository(
    DbContextOptions<ExercisesContext> dbContextOptions
)
{
    public async Task<List<TopicEntity>> GetEverything()
    {
        await using ExercisesContext ctx = new(dbContextOptions);
        return await ctx.Topics
            .Include(topic => topic.Books)
            .ThenInclude(book => book.Chapters)
            .ThenInclude(chapter => chapter.Sections)
            .ThenInclude(section => section.Exercises)
            .ToListAsync()
            .ConfigureAwait(false);
    }

    public Either<ExerciseError, TopicEntity> AddNewTopic(TopicEntity input, ExercisesContext ctx)
    {
        try
        {
            ctx.Topics.Add(input);
            ctx.SaveChanges();
            return Right<ExerciseError, TopicEntity>(input);
        }
        catch (Exception e)
        {
            return Left<ExerciseError, TopicEntity>(
                new ExerciseError($"Error happened while creating a new {nameof(TopicEntity)}. Error: {e.Message}"));
        }
    }
}