namespace Exercises.Logic.Repository.Topic;

using Common;
using Models;
using static Prelude;

public partial class TopicRepository
{
    public Either<ExerciseError, Option<TopicEntity>> FindByName(
        string name,
        ExercisesContext dbContext
    )
    {
        try
        {
            TopicEntity? result = dbContext.Topics.FirstOrDefault(t => t.Name == name);
            return Right(result == null ? Option<TopicEntity>.None : Option<TopicEntity>.Some(result));
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError($"There is no {nameof(TopicEntity)} with name: {name}.")
            );
        }
    }
}