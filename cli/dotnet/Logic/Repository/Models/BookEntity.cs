namespace Exercises.Logic.Repository.Models;

public class BookEntity
{
    public int Id { get; set; }
    public string Title { get; set; }

    public string Authors { get; set; }

    public int PageStart { get; set; }

    public int PageEnd { get; set; }

    public string Reference { get; set; }
    public int TopicId { get; set; }

    // public List<Chapter> Chapters { get; set; } = [];
}