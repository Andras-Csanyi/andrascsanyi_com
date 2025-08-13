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

    public Either<ExerciseError, TopicEntity> AddNewTopic(
        TopicEntity input,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Topics.Add(input);
            ctx.SaveChanges();
            Console.WriteLine($"topic added! ID: {input.Id}");
            return Right(input);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error in repository: {e.Message}");
            return Left(
                new ExerciseError($"Error happened while creating a new {nameof(TopicEntity)}. Error: {e.Message}"));
        }
    }
}