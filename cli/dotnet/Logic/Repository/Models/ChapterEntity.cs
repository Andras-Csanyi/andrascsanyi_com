namespace Exercises.Logic.Repository.Models;

public class ChapterEntity
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string Reference { get; set; }

    public int PageStart { get; set; }

    public int PageEnd { get; set; }
    public int BookId { get; set; }

    // public List<Section> Sections { get; set; } = [];
}