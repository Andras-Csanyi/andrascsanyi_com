namespace Exercises.Logic.CatalogParser.Model;

using Common;
using Repository.Models;
using YamlDotNet.Serialization;
using static Prelude;

public class Chapter
{
    public string? Title { get; set; }

    public string? Reference { get; set; }

    [YamlMember(Alias = "page_start", ApplyNamingConventions = false)]
    public int PageStart { get; set; }

    [YamlMember(Alias = "page_end", ApplyNamingConventions = false)]
    public int PageEnd { get; set; }

    [YamlMember(Alias = "book_reference", ApplyNamingConventions = false)]
    public string BookReference { get; set; }

    public List<Section> Sections { get; set; } = [];
}

public static class ChapterExtensions
{
    public static Either<ExerciseError, ChapterEntity> ToChapterEntity(this Chapter chapter)
    {
        try
        {
            return Right<ExerciseError, ChapterEntity>(
                new ChapterEntity
                {
                    Title = chapter.Title,
                    Reference = chapter.Reference,
                    PageStart = chapter.PageStart,
                    PageEnd = chapter.PageEnd,
                }
            );
        }
        catch (Exception e)
        {
            return Left<ExerciseError, ChapterEntity>(
                new ExerciseError(
                    $"Error happened while mapping {nameof(Chapter)} to {nameof(ChapterEntity)}. Error: {e.Message}"
                )
            );
        }
    }
}