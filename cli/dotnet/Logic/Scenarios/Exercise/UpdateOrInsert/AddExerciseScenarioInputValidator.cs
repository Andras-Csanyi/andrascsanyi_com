namespace Exercises.Logic.Scenarios.Exercise.UpdateOrInsert;

using FluentValidation;
using Repository.Models;

public class AddExerciseScenarioInputValidator : AbstractValidator<ExerciseEntity>
{
    public AddExerciseScenarioInputValidator()
    {
        RuleFor(r => r.Id).Equal(0);
        RuleFor(r => r.IdInTheBook).GreaterThanOrEqualTo(1);
        RuleFor(r => r.SectionId).GreaterThanOrEqualTo(1);
        RuleFor(r => r.SectionIdInThebook).GreaterThanOrEqualTo(1);
        RuleFor(r => r.ChapterId).GreaterThanOrEqualTo(1);
        RuleFor(r => r.ChapterIdInTheBook).GreaterThanOrEqualTo(1);
        RuleFor(r => r.BookId).GreaterThanOrEqualTo(1);
        RuleFor(r => r.TopicId).GreaterThanOrEqualTo(1);
        RuleFor(r => r.ExerciseType).IsInEnum();
    }
}