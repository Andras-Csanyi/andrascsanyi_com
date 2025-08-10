namespace Exercises.Logic.Repository.Models;

public class ChapterEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Reference { get; set; }
    public int PageStart { get; set; }
    public int PageEnd { get; set; }
    public long BookId { get; set; }
    public BookEntity Book { get; set; }

    public List<SectionEntity> Sections { get; set; } = [];
}