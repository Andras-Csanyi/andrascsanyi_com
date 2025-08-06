namespace Exercises.Logic.CatalogParser;

using System.Text.RegularExpressions;
using Logic.CatalogParser.Model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class CatalogParser
{
    private readonly IDeserializer _deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    public StudyTree ParseStudyTree()
    {
        List<string> catalogFiles = ParseDirectory();
        StudyTree studyTreeWithTopics = ParseTopics(catalogFiles);
        StudyTree addedBooks = ParseBooks(catalogFiles, studyTreeWithTopics);
        StudyTree addedChapters = ParseChapters(catalogFiles, addedBooks);
        StudyTree addedSections = ParseSections(catalogFiles, addedChapters);
        return addedSections;
    }

    private T DeserializeYaml<T>(string yaml)
    {
        try
        {
            return _deserializer.Deserialize<T>(yaml);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Deserialization failed at content: {yaml} with error: {e.Message}, trace: {e.StackTrace}");
            throw;
        }
    }

    private string ReadFile(string path)
    {
        try
        {
            return File.ReadAllText(path);
        }
        catch (Exception e)
        {
            Console.WriteLine($"Reading the path: {path} failed with error: {e.Message} and trace: {e.StackTrace}");
            throw;
        }
    }

    private StudyTree ParseSections(List<string> catalogFile, StudyTree studyTree)
    {
        studyTree.Topics.ForEach(topic =>
        {
            topic.Books.ForEach(book =>
            {
                book.Chapters.ForEach(chapter =>
                {
                    catalogFile.Where(file =>
                    {
                        string pattern = $"{topic.Reference}.*{book.Reference}.*{chapter.Reference}.*section.yml";
                        Regex regex = new(pattern, RegexOptions.Compiled);
                        if (regex.IsMatch(file))
                        {
                            return true;
                        }
                        return false;
                    })
                    .ToList()
                    .ForEach(matchedFile =>
                    {
                        string yaml = ReadFile(matchedFile);
                        chapter.Sections.Add(DeserializeYaml<Section>(yaml));
                    });
                });
            });
        });
        return studyTree;
    }

    private StudyTree ParseChapters(List<string> catalogFiles, StudyTree studyTree)
    {
        studyTree.Topics.ForEach(topic =>
        {
            topic.Books.ForEach(book =>
            {
                catalogFiles.Where(file =>
                {
                    string pattern = $"{topic.Reference}.*{book.Reference}.*chapter.yml";
                    Regex regex = new(pattern, RegexOptions.Compiled);
                    if (regex.IsMatch(file))
                    {
                        return true;
                    }
                    return false;
                })
                .ToList()
                .ForEach(matchedFile =>
                {
                    string yaml = ReadFile(matchedFile);
                    book.Chapters.Add(DeserializeYaml<Chapter>(yaml));

                });
            });
        });
        return studyTree;

    }

    private StudyTree ParseBooks(List<string> catalogFiles, StudyTree studyTree)
    {
        Console.WriteLine($"study tree size: {studyTree.Topics.Count}");
        studyTree.Topics.ForEach(topic =>
        {
            catalogFiles.Where(file =>
            {
                string pattern = $"{topic.Reference}.*book.yml";
                Regex regex = new(pattern, RegexOptions.Compiled);
                if (regex.IsMatch(file))
                {
                    return true;
                }
                return false;
            })
            .ToList()
            .ForEach(matchedFile =>
            {
                string yaml = ReadFile(matchedFile);
                topic.Books.Add(DeserializeYaml<Book>(yaml));
            });
        });
        return studyTree;
    }

    private StudyTree ParseTopics(List<string> catalogFiles)
    {
        StudyTree studyTree = new();
        studyTree.Topics = catalogFiles.Where(catalogFile => catalogFile.EndsWith("topic.yml"))
                .ToList()
                .Select(matchedFile =>
                {
                    string yaml = ReadFile(matchedFile);
                    return DeserializeYaml<Topic>(yaml);
                })
                .ToList();
        return studyTree;
    }


    private List<string> ParseDirectory()
    {
        string currDirectory = Directory.GetCurrentDirectory();
        string baseDirectory = GoNLevelsUp(currDirectory, 2);
        string bookDirectory = $"{baseDirectory}/docs/book";
        return ScanDirectoryForCatalogFiles(bookDirectory);
    }

    private List<string> ScanDirectoryForCatalogFiles(string bookDirectory) =>
        Directory.EnumerateFiles(bookDirectory, "*.y*ml", SearchOption.AllDirectories)
            .Where(file => file.EndsWith(".yml", StringComparison.OrdinalIgnoreCase) ||
                           file.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase))
            .Select(Path.GetFullPath)
            .ToList();

    private string GoNLevelsUp(string currentDirectory, int level)
    {
        string normalizedPath = Path.GetFullPath(currentDirectory);
        DirectoryInfo directory = new(normalizedPath);
        for (int i = 1; i <= level; i++)
        {
            directory = directory.Parent;
        }

        return directory.FullName;
    }
}