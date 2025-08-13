namespace Exercises.Logic.Scenarios.Section.Find;

using Common;
using LanguageExt;
using Repository;
using Repository.Models;
using Repository.Section;

public class FindSectionByChapterIdAndSectionNumberScenario(
    SectionRepository sectionRepository
)
{
    public Either<ExerciseError, Option<SectionEntity>> Execute(long chapterId,
        double sectionNumber,
        ExercisesContext ctx) =>
        sectionRepository.FindSectionByChapterIdAndSectionNumber(chapterId, sectionNumber, ctx);
}