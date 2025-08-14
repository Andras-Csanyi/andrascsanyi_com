namespace Exercises.Logic.Repository.Topic;

using Common;
using Models;
using static Prelude;

public partial class TopicRepository
{
    public Either<ExerciseError, Option<TopicEntity>> FindByReference(
        string reference,
        ExercisesContext dbContext
    )
    {
        try
        {
            TopicEntity? result = dbContext.Topics.FirstOrDefault(t => t.Reference == reference);
            return Right(result == null ? Option<TopicEntity>.None : Option<TopicEntity>.Some(result));
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError(
                    $"Error happened while looking for {nameof(TopicEntity)} with name: {reference}. Error: {e.Message}"
                )
            );
        }
    }
}