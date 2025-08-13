namespace Exercises.Logic.CatalogParser.Model;

using Common;
using LanguageExt;
using Repository.Models;
using static LanguageExt.Prelude;

public class Topic
{
    public string? Name { get; set; }

    public string? Reference { get; set; }

    public List<Book> Books { get; set; } = [];
}

public static class TopicExtensions
{
    public static Either<ExerciseError, TopicEntity> ToTopicEntity(
        this Topic topic
    )
    {
        try
        {
            Console.WriteLine($"Mapping Topic: {topic.Name}, {topic.Reference}");
            return Right<ExerciseError, TopicEntity>(new TopicEntity
            {
                Id = 0, Name = topic.Name, Reference = topic.Reference,
            });
        }
        catch (Exception e)
        {
            return Left<ExerciseError, TopicEntity>(
                new ExerciseError($"Mapping of {nameof(TopicEntity)} failed. Error: {e.Message}"));
        }
    }
}