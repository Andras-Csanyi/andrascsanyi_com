namespace Exercises.Logic.Repository.Models;

public class TopicEntity
{
    public long Id { get; set; }
    public string Name { get; set; }
    public string Reference { get; set; }
    public List<BookEntity> Books { get; set; }
}