namespace Exercises.Logic.Scenarios.Topic.Add;

using Common;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using Repository.Models;
using static LanguageExt.Prelude;

public class AddNewTopicScenarioInputValidator : AbstractValidator<TopicEntity>
{
    public AddNewTopicScenarioInputValidator()
    {
        RuleFor(r => r.Id).Equal(0);
        When(w => !string.IsNullOrEmpty(w.Name) && !string.IsNullOrWhiteSpace(w.Name), () =>
        {
            RuleFor(r => r.Name.Trim().Length).GreaterThanOrEqualTo(3);
        });
        When(w => !string.IsNullOrEmpty(w.Reference) && !string.IsNullOrWhiteSpace(w.Reference), () =>
        {
            RuleFor(r => r.Reference.Trim().Length).GreaterThanOrEqualTo(3);
        });
    }

    public Either<ExerciseError, TopicEntity> IsValid(TopicEntity input)
    {
        ValidationResult? result = Validate(input);
        if (result.IsValid)
        {
            return Right<ExerciseError, TopicEntity>(input);
        }

        return Left<ExerciseError, TopicEntity>(new ExerciseError(result.ToErrorString()));
    }
}