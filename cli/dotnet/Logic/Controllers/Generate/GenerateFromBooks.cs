namespace Exercises.Logic.Controllers.Generate;

using CatalogParser;
using CatalogParser.Model;
using Common;
using Repository.Topic;
using Sync;

public class GenerateFromBooks(
    SyncFsWithDb syncFsWithDb,
    TopicRepository topicRepository
)
{
    public void Execute(
        GenerateBooksCommandParameters generateFromBooksParams
    )
    {
        CatalogParser catalogParser = new();
        StudyTree parsedStudyTree = catalogParser.ParseStudyTree();
        Console.WriteLine($"size: {parsedStudyTree.Topics.Count}");
        syncFsWithDb.Execute(parsedStudyTree);
        // // List<TopicEntity> topics = await topicRepository.GetEverything();
    }
}