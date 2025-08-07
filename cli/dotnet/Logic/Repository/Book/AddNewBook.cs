namespace Exercises.Logic.Repository.Book;

using Exercises.Logic.Repository.Models;
using LanguageExt;
using static LanguageExt.Prelude;

public partial class BookRepository
{
    public async Task<Either<RepositoryError, BookEntity>> AddNewBookAsync(
            BookEntity input,
            int topicId,
            CancellationToken cancellationToken = default)
    {
        try
        {
            using ExercisesContext ctx = new(dbContextOptions);
            BookEntity newBook = new()
            {
                Title = input.Title,
                Authors = input.Authors,
                PageStart = input.PageStart,
                PageEnd = input.PageEnd,
                Reference = input.Reference,
                TopicId = topicId,
            };
            await ctx.Books.AddAsync(newBook, cancellationToken).ConfigureAwait(false);
            await ctx.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            return Right(newBook);
        }
        catch (Exception e)
        {
            return Left(
                    new RepositoryError($"Error adding book: {e.Message}",
                        $"Inner exception: {e.InnerException?.Message}"
                    ));

        }
    }
}