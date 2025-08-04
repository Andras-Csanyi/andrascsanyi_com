namespace Logic.CatalogParser.Model;

using YamlDotNet.Serialization;

public class Book
{
    public string? Title { get; set; }

    public string? Authors { get; set; }

    [YamlMember(Alias = "page_start", ApplyNamingConventions = false)]
    public int PageStart { get; set; }

    [YamlMember(Alias = "page_end", ApplyNamingConventions = false)]
    public int PageEnd { get; set; }

    public string? Reference { get; set; }

    public List<Chapter> Chapters { get; set; } = [];
}
