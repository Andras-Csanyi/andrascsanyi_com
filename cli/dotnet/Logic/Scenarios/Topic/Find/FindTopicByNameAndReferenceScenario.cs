namespace Exercises.Logic.Scenarios.Topic.Find;

using Common;
using LanguageExt;
using Repository;
using Repository.Models;
using Repository.Topic;
using static LanguageExt.Prelude;

public class FindTopicByNameAndReferenceScenario(TopicRepository topicRepository)
{
    public Either<ExerciseError, Option<TopicEntity>> Execute(
        string name,
        string reference,
        ExercisesContext dbContext
    )
    {
        if (string.IsNullOrWhiteSpace(name)
            || string.IsNullOrEmpty(name)
            || string.IsNullOrWhiteSpace(reference)
            || string.IsNullOrEmpty(reference))
        {
            return Left(new ExerciseError($"Either name or reference wasn't provided."));
        }

        return from r in topicRepository.FindByNameAndReference(name, reference, dbContext)
            select r;
    }
}