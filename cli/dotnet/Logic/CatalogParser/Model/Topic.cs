namespace Logic.CatalogParser.Model;

public class Topic
{
    public string? Name { get; set; }

    public string? Reference { get; set; }

    public List<Book> Books { get; set; } = [];
}
