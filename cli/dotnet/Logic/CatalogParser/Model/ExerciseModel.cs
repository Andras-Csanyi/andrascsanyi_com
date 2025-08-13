namespace Exercises.Logic.CatalogParser.Model;

public record ExerciseRecord
{
    public IEnumerable<Topic> Topics { get; set; }
    public IEnumerable<Book> Books { get; set; }
    public IEnumerable<Chapter> Chapters { get; set; }
    public IEnumerable<Section> Sections { get; set; }
}