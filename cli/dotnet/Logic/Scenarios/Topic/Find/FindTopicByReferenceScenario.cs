namespace Exercises.Logic.Scenarios.Topic.Find;

using Common;
using Repository;
using Repository.Models;
using Repository.Topic;
using static Prelude;

public class FindTopicByReferenceScenario(
    TopicRepository topicRepository)
{
    public Either<ExerciseError, Option<TopicEntity>> Execute(
        string reference,
        ExercisesContext dbContext
    )
    {
        if (string.IsNullOrWhiteSpace(reference)
            || string.IsNullOrEmpty(reference))
        {
            return Left(new ExerciseError($"Either name or reference wasn't provided."));
        }

        return from r in topicRepository.FindByReference(reference, dbContext)
            select r;
    }
}