namespace Exercises.Logic.Scenarios.Book.Find;

using Common;
using Repository;
using Repository.Book;
using Repository.Models;

public class FindBookByReferenceScenario(
    BookRepository bookRepository)
{
    public Either<ExerciseError, Option<BookEntity>> Execute(
        string reference,
        ExercisesContext dbContext
    ) =>
        from r in bookRepository.FindByReference(reference, dbContext)
        select r;
}