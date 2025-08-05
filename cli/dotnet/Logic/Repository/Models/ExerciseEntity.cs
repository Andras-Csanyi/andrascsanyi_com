namespace Exercises.Logic.Repository.Models
{
    public class ExerciseEntity
    {
        public int Id { get; set; }
        public int IdInTheBook { get; set; }
        public int SectionId { get; set; }
        public double SectionIdInThebook { get; set; }
        public int ChapterId { get; set; }
        public double ChapterIdInTheBook { get; set; }
        public int BookId { get; set; }
        public int TopicId { get; set; }
        public ExerciseType ExerciseType { get; set; }
    }

    public enum ExerciseType
    {
        Concept,
        Skill,
        Application,
        Discussion,
    }
}