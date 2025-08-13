namespace Exercises.Logic.Repository.Section;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class SectionRepository
{
    public Either<ExerciseError, Option<SectionEntity>> FindSectionByChapterIdAndSectionNumber(
        long chapterId,
        double sectionNumber,
        ExercisesContext ctx)
    {
        try
        {
            SectionEntity? target = ctx.Sections
                .FirstOrDefault(w => w.ChapterId == chapterId && w.SectionNumber == sectionNumber);
            return target == null ? None : Some(target);

        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError($"Error happened while looking " +
                                  $"for {nameof(SectionEntity)} by " +
                                  $"{nameof(ChapterEntity)}.{nameof(ChapterEntity.Id)} with value: {chapterId} and " +
                                  $"{nameof(SectionEntity.SectionNumber)} with value: {sectionNumber}. " +
                                  $"Error: {e.Message}"));
        }
    }
}