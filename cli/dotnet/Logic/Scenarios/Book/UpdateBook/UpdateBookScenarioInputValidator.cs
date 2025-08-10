namespace Exercises.Logic.Scenarios.Book.UpdateBook;

using Common;
using FluentValidation;
using FluentValidation.Results;
using LanguageExt;
using Repository.Models;
using static LanguageExt.Prelude;

public class UpdateBookScenarioInputValidator : AbstractValidator<BookEntity>
{
    public UpdateBookScenarioInputValidator()
    {
        RuleFor(r => r.Id).GreaterThanOrEqualTo(1);
        RuleFor(r => r.TopicId).GreaterThanOrEqualTo(1);
        When(book => book.Title != null, () =>
        {
            RuleFor(book => book.Title).NotEmpty();
            RuleFor(book => book.Title.Trim().Length).GreaterThanOrEqualTo(3);
        });
        When(book => book.Reference != null, () =>
        {
            RuleFor(book => book.Reference).NotEmpty();
            RuleFor(book => book.Reference.Trim().Length).GreaterThanOrEqualTo(3);
        });
        When(book => book.Authors != null, () =>
        {
            RuleFor(book => book.Authors).NotEmpty();
            RuleFor(book => book.Authors.Trim().Length).GreaterThanOrEqualTo(3);
        });
    }

    public Either<ExerciseError, BookEntity> IsValid(BookEntity input)
    {
        ValidationResult? validationResult = Validate(input);
        if (validationResult.IsValid)
        {
            return Right<ExerciseError, BookEntity>(input);
        }

        return Left<ExerciseError, BookEntity>(new ExerciseError(validationResult.ToErrorString()));
    }
}