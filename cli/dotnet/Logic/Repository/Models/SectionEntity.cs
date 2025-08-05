namespace Exercises.Logic.Repository.Models;

public class SectionEntity
{
    public int Id { get; set; }
    public string? Title { get; set; }

    public double SectionNumber { get; set; }

    public int PageStart { get; set; }

    public int PageExercisesStart { get; set; }

    public int ConceptQuestionsIntervalStart { get; set; }

    public int ConceptQuestionsIntervalEnd { get; set; }

    public int SkillQuestionsIntervalStart { get; set; }

    public int SkillQuestionsIntervalEnd { get; set; }

    public int ApplicationQuestionsIntervalStart { get; set; }

    public int ApplicationQuestionsIntervalEnd { get; set; }

    public int DiscussionQuestionsIntervalStart { get; set; }

    public int DiscussionQuestionsIntervalEnd { get; set; }

    public int PageEnd { get; set; }
    public int ChapterId { get; set; }
    public ChapterEntity Chapter { get; set; }
}