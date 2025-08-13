namespace Exercises.Logic.CatalogParser;

using System.Text.RegularExpressions;
using Common;
using Model;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;

public class CatalogParser
{
    private static readonly IDeserializer Deserializer = new DeserializerBuilder()
        .WithNamingConvention(CamelCaseNamingConvention.Instance)
        .Build();

    public static Either<ExerciseError, ExerciseRecord> Parse() =>
        from catalogFiles in ParseDirectory()
        from exercisesWithTopics in ParseTopics(catalogFiles)
        from booksAdded in ParseBooks(catalogFiles, exercisesWithTopics)
        from chaptersAdded in ParseChapters(catalogFiles, booksAdded)
        from sectionsAdded in ParseSections(catalogFiles, chaptersAdded)
        select sectionsAdded;

    private static Either<ExerciseError, T> DeserializeYaml<T>(
        string yaml
    )
    {
        try
        {
            T result = Deserializer.Deserialize<T>(yaml);
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError(
                    $"Deserialization failed at content: {yaml} with error: {e.Message}, trace: {e.StackTrace}"
                )
            );
        }
    }

    private static Either<ExerciseError, string> ReadFile(
        string path
    )
    {
        try
        {
            string result = File.ReadAllText(path);
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(
                new ExerciseError(
                    $"Reading the path: {path} failed with error: {e.Message} and trace: {e.StackTrace}"
                )
            );
        }
    }

    private static Either<ExerciseError, ExerciseRecord> ParseSections(
        List<string> catalogFile,
        ExerciseRecord exerciseRecord
    )
    {
        IEnumerable<string> sectionFiles = from files in catalogFile.Where(f => f.EndsWith("section.yml")).ToList()
            select files;
        IEnumerable<Section> parsedSectionFiles = from parsed in toSeq(sectionFiles).FoldWhile(
                new List<Section>(),
                (List<Section> state, string path) =>
                {
                    Either<ExerciseError, Section> parsedResult = from readFile in ReadFile(path)
                        from parsedFile in DeserializeYaml<Section>(readFile)
                        select parsedFile;
                    return parsedResult.Match(
                        Right: rightResult =>
                        {
                            state.Add(rightResult);
                            return state;
                        },
                        Left: _ => state
                    );
                },
                _ => true
            )
            select parsed;
        return exerciseRecord with { Sections = parsedSectionFiles, };
    }

    private static Either<ExerciseError, ExerciseRecord> ParseChapters(
        List<string> catalogFiles,
        ExerciseRecord exerciseRecord
    )
    {
        IEnumerable<string> filePaths = from files in catalogFiles.Where(file =>
                {
                    string pattern = $"*chapter.yml";
                    Regex regex = new(pattern, RegexOptions.Compiled);
                    return regex.IsMatch(file);
                }
            )
            select files;
        IEnumerable<Chapter> parsedChapters = from file in toSeq(filePaths).FoldWhile(
                new List<Chapter>(),
                (List<Chapter> state, string filePath) =>
                {
                    Either<ExerciseError, Chapter> parsedResult = from readFile in ReadFile(filePath)
                        from parsedFile in DeserializeYaml<Chapter>(readFile)
                        select parsedFile;
                    return parsedResult.Match(
                        Right: rightResult =>
                        {
                            state.Add(rightResult);
                            return state;
                        },
                        Left: _ => state
                    );
                },
                _ => true
            )
            select file;
        return exerciseRecord with { Chapters = parsedChapters, };
    }

    private static Either<ExerciseError, ExerciseRecord> ParseBooks(
        List<string> catalogFiles,
        ExerciseRecord exerciseRecord
    )
    {
        IEnumerable<string> bookPathList = from bookFilePathList in catalogFiles.Where(file =>
                {
                    string pattern = $".*book.yml";
                    Regex regex = new(pattern, RegexOptions.Compiled);
                    return regex.IsMatch(file);
                }
            ).ToList()
            select bookFilePathList;
        IEnumerable<Book> parsedBooks = from books in toSeq(bookPathList).FoldWhile(
                new List<Book>(),
                (List<Book> state, string filePath) =>
                {
                    Either<ExerciseError, Book> result = from readFile in ReadFile(filePath)
                        from parsedFile in DeserializeYaml<Book>(readFile)
                        select parsedFile;
                    return result.Match(
                        Right: rightResult =>
                        {
                            state.Add(rightResult);
                            return state;
                        },
                        Left: _ => state
                    );
                },
                _ => true
            )
            select books;
        return exerciseRecord with { Books = parsedBooks, };
    }

    private static Either<ExerciseError, ExerciseRecord> ParseTopics(
        List<string> catalogFiles
    )
    {
        IEnumerable<string> res = from list in toSeq(catalogFiles).Filter(file => file.EndsWith("topic.yml")).ToList()
            select list;
        IEnumerable<Topic> parsedFilesResult = from parsedFiles in toSeq(res).FoldWhile(
                new List<Topic>(),
                (List<Topic> state, string filePath) =>
                {
                    Either<ExerciseError, Topic> result = from readFile in ReadFile(filePath)
                        from parsedFile in DeserializeYaml<Topic>(readFile)
                        select parsedFile;
                    return result.Match(
                        Right: right =>
                        {
                            state.Add(right);
                            return state;
                        },
                        Left: _ => state
                    );
                },
                _ => true
            )
            select parsedFiles;
        ExerciseRecord exerciseModel = new() { Topics = parsedFilesResult, };
        return exerciseModel;
    }

    private static Either<ExerciseError, List<string>> ParseDirectory() =>
        from currentDirectory in GetCurrentDirectory()
        from baseDirectory in GoNLevelsUp(currentDirectory, 2)
        let bookDirectory = $"{baseDirectory}/docs/book"
        from catalogFiles in ScanDirectoryForCatalogFiles(bookDirectory)
        select catalogFiles;

    private static Either<ExerciseError, string> GetCurrentDirectory()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            return Right(currentDirectory);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }

    private static Either<ExerciseError, List<string>> ScanDirectoryForCatalogFiles(
        string bookDirectory
    )
    {
        try
        {
            List<string> result = Directory.EnumerateFiles(bookDirectory, "*.y*ml", SearchOption.AllDirectories)
                .Where(file => file.EndsWith(".yml", StringComparison.OrdinalIgnoreCase) ||
                               file.EndsWith(".yaml", StringComparison.OrdinalIgnoreCase)
                )
                .Select(Path.GetFullPath)
                .ToList();
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }

    private static Either<ExerciseError, string> GoNLevelsUp(
        string currentDirectory,
        int level
    )
    {
        try
        {
            string normalizedPath = Path.GetFullPath(currentDirectory);
            DirectoryInfo directory = new(normalizedPath);
            for (int i = 1; i <= level; i++)
            {
                directory = directory.Parent;
            }

            return Right(directory.FullName);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}