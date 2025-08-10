namespace Exercises.Logic.Repository.Topic;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class TopicRepository
{
    public Either<ExerciseError, Option<TopicEntity>> FindByNameAndReference(
        string name,
        string reference,
        ExercisesContext dbContext
    )
    {
        try
        {
            TopicEntity? result = dbContext.Topics
                .FirstOrDefault(t => t.Name == name && t.Reference == reference);
            if (result == null)
            {
                return Right(Option<TopicEntity>.None);
            }

            return Right(Option<TopicEntity>.Some(result));
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError($"There is no {nameof(TopicEntity)} with name: {name} and reference: {reference}."));
        }
    }
}