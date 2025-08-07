namespace Exercises.Logic.Repository.Book;

using Exercises.Logic.Repository.Models;
using FluentValidation;

public class AddNewBookInputValidation : AbstractValidator<BookEntity>
{
    public AddNewBookInputValidation()
    {
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
}