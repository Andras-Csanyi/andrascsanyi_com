namespace Exercises.Logic.Scenarios.Book.Find;

using Common;
using LanguageExt;
using Repository;
using Repository.Book;
using Repository.Models;

public class FindBookByTopicIdAndReferenceScenario(
    BookRepository bookRepository)
{
    public Either<ExerciseError, BookEntity> Execute(long topicId, string reference, ExercisesContext dbContext) =>
        from r in bookRepository.FindByTopicIdAndReference(topicId, reference, dbContext)
        select r;
}