namespace Exercises.Logic.Scenarios.Book.UpdateBook;

using CatalogParser.Model;
using Common;
using LanguageExt;
using Repository;
using Repository.Book;
using Repository.Models;
using static LanguageExt.Prelude;

public class UpdateBookScenario(
    UpdateBookScenarioInputValidator validator,
    BookRepository bookRepository
)
{
    public Either<ExerciseError, BookEntity> Execute(long bookId, Book parsedBook, ExercisesContext ctx) =>
        from mappedInput in MapInputToEntity(parsedBook, bookId)
        from validatedInput in ValidateInputEntity(mappedInput)
        from updatedBookEntity in UpdateBookEntity(validatedInput, ctx)
        select updatedBookEntity;

    private Either<ExerciseError, BookEntity> UpdateBookEntity(BookEntity updated, ExercisesContext ctx) =>
        bookRepository.UpdateBook(updated, ctx);


    private Either<ExerciseError, BookEntity> MapInputToEntity(Book parsedBook, long bookId) =>
        parsedBook.ToBookEntity().Match(
            result =>
            {
                result.Id = bookId;
                return Right<ExerciseError, BookEntity>(result);
            },
            () => Left<ExerciseError, BookEntity>(new ExerciseError("Mapping has failed.")));

    private Either<ExerciseError, BookEntity> ValidateInputEntity(BookEntity inputEntity) =>
        validator.IsValid(inputEntity);
}