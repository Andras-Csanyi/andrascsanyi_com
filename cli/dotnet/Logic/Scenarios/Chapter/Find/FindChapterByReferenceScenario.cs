namespace Exercises.Logic.Scenarios.Chapter.Find;

using Common;
using Repository;
using Repository.Chapter;
using Repository.Models;

public class FindChapterByReferenceScenario(
    ChapterRepository chapterRepository
)
{
    public Either<ExerciseError, Option<ChapterEntity>> Execute(
        string reference,
        ExercisesContext ctx
    ) =>
        from countOfChaptersByReference in chapterRepository.CountByReference(reference, ctx)
        let isOne = match(
            countOfChaptersByReference == 1
                ? Either<ExerciseError, int>.Right(1)
                : Either<ExerciseError, int>.Left(
                    new ExerciseError(
                        $"Requesting chapter by reference: {reference} resulted in {countOfChaptersByReference}. " +
                        $"This should be 1."
                    )
                ),
            nopes => Left(nopes),
            yolo => Right(yolo)
        )
        from chapterEntity in chapterRepository.FindByReference(reference, ctx)
        select chapterEntity;
}