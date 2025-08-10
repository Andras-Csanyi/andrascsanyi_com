namespace Exercises.Logic.Repository.Book;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class BookRepository
{
    public Either<ExerciseError, Unit> AddNewBookEntity(BookEntity validatedInput,
        ExercisesContext dbContext)
    {
        try
        {
            dbContext.Books.Add(validatedInput);
            return Right<ExerciseError, Unit>(Unit.Default);
        }
        catch (Exception e)
        {
            return Left<ExerciseError, Unit>(new ExerciseError($"{e.Message}"));
        }
    }
}