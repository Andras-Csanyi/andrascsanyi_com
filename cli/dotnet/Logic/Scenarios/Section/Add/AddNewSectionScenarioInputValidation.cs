namespace Exercises.Logic.Scenarios.Section.Add;

using Common;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using Repository.Models;
using static LanguageExt.Prelude;

public class AddNewSectionScenarioInputValidation : AbstractValidator<SectionEntity>
{
    public AddNewSectionScenarioInputValidation()
    {
        RuleFor(r => r.Id).Equal(0);
        When(w => !string.IsNullOrEmpty(w.Title) && !string.IsNullOrWhiteSpace(w.Title), () =>
        {
            RuleFor(r => r.Title!.Trim().Length).GreaterThanOrEqualTo(3);
        });
        RuleFor(r => r.SectionNumber).GreaterThanOrEqualTo(0);
        RuleFor(r => r.PageStart).GreaterThanOrEqualTo(0);
        RuleFor(r => r.PageExercisesStart).GreaterThanOrEqualTo(0);
        RuleFor(r => r.ConceptQuestionsIntervalStart).GreaterThanOrEqualTo(0);
        RuleFor(r => r.ConceptQuestionsIntervalEnd).GreaterThanOrEqualTo(0);
        RuleFor(r => r.SkillQuestionsIntervalStart).GreaterThanOrEqualTo(0);
        RuleFor(r => r.SkillQuestionsIntervalEnd).GreaterThanOrEqualTo(0);
        RuleFor(r => r.ApplicationQuestionsIntervalStart).GreaterThanOrEqualTo(0);
        RuleFor(r => r.ApplicationQuestionsIntervalEnd).GreaterThanOrEqualTo(0);
        RuleFor(r => r.DiscussionQuestionsIntervalStart).GreaterThanOrEqualTo(0);
        RuleFor(r => r.DiscussionQuestionsIntervalEnd).GreaterThanOrEqualTo(0);
        RuleFor(r => r.PageEnd).GreaterThanOrEqualTo(0);
        RuleFor(r => r.ChapterId).GreaterThanOrEqualTo(0);
    }

    public Either<ExerciseError, SectionEntity> IsValid(SectionEntity entity)
    {
        ValidationResult? validationResult = Validate(entity);
        if (validationResult != null && !validationResult.IsValid)
        {
            return Left(new ExerciseError($"Invalid {nameof(SectionEntity)}"));
        }

        return Right(entity);
    }
}