namespace Exercises.Logic.Repository.Topic;

using System.Collections.Generic;
using Exercises.Logic.Repository.Models;
using Microsoft.EntityFrameworkCore;

public class TopicRepository(
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
}