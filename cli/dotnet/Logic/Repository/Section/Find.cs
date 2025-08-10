namespace Exercises.Logic.Repository.Section;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class SectionRepository
{
    public Either<ExerciseError, SectionEntity> FindSectionByChapterIdAndSectionNumber(
        long chapterId,
        double sectionNumber,
        ExercisesContext ctx)
    {
        try
        {
            SectionEntity? target = ctx.Sections
                .First(w => w.ChapterId == chapterId && w.SectionNumber == sectionNumber);
            return Right(target);
        }
        catch (Exception e)
        {
            return Left<ExerciseError, SectionEntity>(
                new ExerciseError($"Error happened while looking " +
                                  $"for {nameof(SectionEntity)} by " +
                                  $"{nameof(ChapterEntity)}.{nameof(ChapterEntity.Id)} with value: {chapterId} and " +
                                  $"{nameof(SectionEntity.SectionNumber)} with value: {sectionNumber}. " +
                                  $"Error: {e.Message}"));
        }
    }
}