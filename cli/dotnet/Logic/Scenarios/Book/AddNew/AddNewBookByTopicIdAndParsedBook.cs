namespace Exercises.Logic.Scenarios.Book.AddNew;

using CatalogParser.Model;
using Common;
using LanguageExt;
using Repository;
using Repository.Book;
using Repository.Models;
using static LanguageExt.Prelude;

public class AddNewBookByTopicIdAndParsedBook(
    BookRepository bookRepository,
    AddNewBookScenarioInputValidator validator
)
{
    public Either<ExerciseError, BookEntity> Execute(
        long topicId,
        Book parsedBook,
        ExercisesContext dbContext
    ) =>
        from mappedInput in MapToBookEntity(parsedBook, topicId)
        from validatedInput in ValidateInputEntity(mappedInput)
        from recordedEntity in SaveNewEntity(validatedInput, dbContext)
        select recordedEntity;

    private Either<ExerciseError, BookEntity> MapToBookEntity(
        Book parsedBook,
        long topicId
    ) =>
        parsedBook.ToBookEntity().Match(
            val =>
            {
                val.TopicId = topicId;
                return Right<ExerciseError, BookEntity>(val);
            },
            () => Left<ExerciseError, BookEntity>(new ExerciseError("Failed mapping")));

    private Either<ExerciseError, BookEntity> SaveNewEntity(
        BookEntity validatedInput,
        ExercisesContext context
    ) =>
        from result in bookRepository.AddNewBookEntity(validatedInput, context)
        select result;

    private Either<ExerciseError, BookEntity> ValidateInputEntity(
        BookEntity mappedInput
    ) =>
        validator.IsValid(mappedInput);
}