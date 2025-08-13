namespace Exercises.Logic.CatalogParser.Model;

using Repository.Models;
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

    [YamlMember(Alias = "topic_reference", ApplyNamingConventions = false)]
    public string? TopicReference { get; set; }

    public List<Chapter> Chapters { get; set; } = [];
}

public static class BookExtensions
{
    public static Option<BookEntity> ToBookEntity(this Book parsedBook)
    {
        try
        {
            return Option<BookEntity>.Some(
                new BookEntity
                {
                    Title = parsedBook.Title,
                    Authors = parsedBook.Authors,
                    PageStart = parsedBook.PageStart,
                    PageEnd = parsedBook.PageEnd,
                    Reference = parsedBook.Reference,
                }
            );
        }
        catch (Exception e)
        {
            return Option<BookEntity>.None;
        }
    }
}