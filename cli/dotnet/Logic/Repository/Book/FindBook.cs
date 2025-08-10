namespace Exercises.Logic.Repository.Book;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class BookRepository
{
    public Either<ExerciseError, BookEntity> FindByTopicIdAndReference(
        long topicId,
        string reference,
        ExercisesContext dbContext)
    {
        try
        {
            BookEntity existingBook = dbContext.Books
                .First(b => b.Reference == reference && b.TopicId == topicId);
            return Right(existingBook);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(
                $"No book found with reference {reference}"
            ));
        }
    }
}