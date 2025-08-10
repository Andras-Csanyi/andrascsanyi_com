namespace Exercises.Logic.Repository.Models;

public class BookEntity
{
    public long Id { get; set; }
    public string Title { get; set; }
    public string Authors { get; set; }
    public long PageStart { get; set; }
    public long PageEnd { get; set; }
    public string Reference { get; set; }
    public long TopicId { get; set; }
    public TopicEntity Topic { get; set; }

    public List<ChapterEntity> Chapters { get; set; } = [];
}