namespace Exercises.Logic.Repository.Book;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class BookRepository
{
    public Either<ExerciseError, BookEntity> AddNewBookEntity(
        BookEntity input,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Books.Add(input);
            ctx.SaveChanges();
            return Right(input);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError($"{e.Message}"));
        }
    }
}