namespace Exercises.Logic.Repository.Models;

public class ExerciseEntity
{
    public long Id { get; set; }
    public long IdInTheBook { get; set; }
    public long SectionId { get; set; }
    public SectionEntity Section { get; set; }
    public double SectionIdInThebook { get; set; }
    public long ChapterId { get; set; }
    public ChapterEntity Chapter { get; set; }
    public double ChapterIdInTheBook { get; set; }
    public long BookId { get; set; }
    public BookEntity Book { get; set; }
    public long TopicId { get; set; }
    public TopicEntity Topic { get; set; }
    public ExerciseType ExerciseType { get; set; }
}

public enum ExerciseType
{
    Concept,
    Skill,
    Application,
    Discussion,
}