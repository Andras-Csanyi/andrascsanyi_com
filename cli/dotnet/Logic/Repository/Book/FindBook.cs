namespace Exercises.Logic.Repository.Book;

using Exercises.Logic.Repository.Models;
using LanguageExt;
using LanguageExt;
using Microsoft.EntityFrameworkCore;
using static LanguageExt.Prelude;

public partial class BookRepository
{
    public async Task<Either<RepositoryError, BookEntity>> FindBookAsync(
            string reference,
            CancellationToken cancellationToken = default)
    {
        await using ExercisesContext ctx = new(dbContextOptions);

        BookEntity? existingBook = await ctx.Books
            .FirstOrDefaultAsync(b => b.Reference == reference)
            .ConfigureAwait(false);

        if (existingBook == null)
        {
            return Left(new RepositoryError(
                        $"No book found with reference {reference}",
                        $"No book found with reference {reference}"
                        ));
        }
        return Right(existingBook);
    }
}