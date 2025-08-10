namespace Exercises.Logic.Scenarios.Topic.Add;

using CatalogParser.Model;
using Common;
using LanguageExt;
using Repository;
using Repository.Models;
using Repository.Topic;

public class AddNewTopicScenario(
    AddNewTopicScenarioInputValidator validator,
    TopicRepository topicRepository
)
{
    public Either<ExerciseError, TopicEntity> Execute(Topic parsedTopic, ExercisesContext ctx) =>
        from mappedInput in MapInputToEntity(parsedTopic)
        from validatedInput in ValidateInput(mappedInput)
        from newTopic in SaveNewTopic(validatedInput, ctx)
        select newTopic;

    private Either<ExerciseError, TopicEntity> SaveNewTopic(TopicEntity input, ExercisesContext ctx) =>
        topicRepository.AddNewTopic(input, ctx);

    private Either<ExerciseError, TopicEntity> MapInputToEntity(Topic parsedTopic) =>
        parsedTopic.ToTopicEntity();

    private Either<ExerciseError, TopicEntity> ValidateInput(TopicEntity topicEntity) =>
        validator.IsValid(topicEntity);
}