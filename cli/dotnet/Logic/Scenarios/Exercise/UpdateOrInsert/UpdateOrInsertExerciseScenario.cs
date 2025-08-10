namespace Exercises.Logic.Scenarios.Exercise.UpdateOrInsert;

using Common;
using LanguageExt;
using Repository;
using Repository.Exercise;
using Repository.Models;

public class UpdateOrInsertExerciseScenario(
    UpdateExerciseScenarioInputValidator updateExerciseScenarioInputValidator,
    AddExerciseScenarioInputValidator addExerciseScenarioInputValidator,
    ExerciseRepository exerciseRepository
)
{
    public Either<ExerciseError, ExerciseEntity> Execute(
        long topicId,
        long bookId,
        long chapterId,
        long chapterIdInTheBook,
        long sectionId,
        double sectionIdInTheBook,
        long idInTheBook,
        ExerciseType exerciseType,
        ExercisesContext ctx
    ) =>
        from mappedInput in MapInputToEntity(topicId, bookId, chapterId, chapterIdInTheBook, sectionId,
            sectionIdInTheBook, idInTheBook, exerciseType)
        from findResult in FindEntity(mappedInput, ctx).Match(
            Right: update =>
            {
                Either<ExerciseError, ExerciseEntity> result = exerciseRepository.Update(update.Id, mappedInput, ctx);
                return result;
            },
            Left: _ =>
            {
                Either<ExerciseError, ExerciseEntity> result = exerciseRepository.Add(mappedInput, ctx);
                return result;
            })
        select findResult;


    private Either<ExerciseError, ExerciseEntity> UpdateEntity(
        ExerciseEntity entity,
        ExercisesContext ctx
    ) =>
        exerciseRepository.Update(entity.Id, entity, ctx);

    private Either<ExerciseError, ExerciseEntity> FindEntity(
        ExerciseEntity mappedInput,
        ExercisesContext ctx
    ) =>
        exerciseRepository.Find(mappedInput, ctx)
            .Match(
                Right: result => Right<ExerciseError, ExerciseEntity>(result),
                Left: _ => Left<ExerciseError, ExerciseEntity>(new ExerciseError($"")));

    private Either<ExerciseError, ExerciseEntity> MapInputToEntity(
        long topicId,
        long bookId,
        long chapterId,
        long chapterIdInTheBook,
        long sectionId,
        double sectionIdInTheBook,
        long idInTheBook,
        ExerciseType exerciseType
    ) =>
        new ExerciseEntity()
        {
            Id = 0,
            IdInTheBook = idInTheBook,
            SectionId = sectionId,
            SectionIdInThebook = sectionIdInTheBook,
            ChapterId = chapterId,
            ChapterIdInTheBook = chapterIdInTheBook,
            BookId = bookId,
            TopicId = topicId,
            ExerciseType = exerciseType,
        };
}