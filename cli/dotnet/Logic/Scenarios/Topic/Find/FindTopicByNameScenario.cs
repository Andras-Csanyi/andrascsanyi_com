namespace Exercises.Logic.Scenarios.Topic.Find;

using Common;
using Repository;
using Repository.Models;
using Repository.Topic;
using static Prelude;

public class FindTopicByNameScenario(
    TopicRepository topicRepository)
{
    public Either<ExerciseError, Option<TopicEntity>> Execute(
        string name,
        ExercisesContext dbContext
    )
    {
        if (string.IsNullOrWhiteSpace(name)
            || string.IsNullOrEmpty(name))
        {
            return Left(new ExerciseError($"Either name or reference wasn't provided."));
        }

        return from r in topicRepository.FindByName(name, dbContext)
            select r;
    }
}