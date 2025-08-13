namespace Exercises.Logic.Scenarios.Topic.Find;

using Common;
using Repository;
using Repository.Models;
using Repository.Topic;

public class GetAllTopicsScenario(
    TopicRepository repository
)
{
    public Either<ExerciseError, List<TopicEntity>> Execute(
        ExercisesContext ctx
    ) =>
        from all in repository.FindAll(ctx)
        select all;
}