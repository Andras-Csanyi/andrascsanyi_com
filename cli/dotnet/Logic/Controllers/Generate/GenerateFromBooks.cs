namespace Exercises.Logic.Controllers.Generate;

using Exercises.Common;
using Exercises.Logic.CatalogParser.Model;
using Exercises.Logic.Repository.Models;
using Exercises.Logic.Repository.Topic;
using Exercises.Logic.Sync;

public class GenerateFromBooks(
        SyncFsWithDb syncFsWithDb,
        TopicRepository topicRepository
        )
{
    public async Task Execute(GenerateBooksCommandParameters generateFromBooksParams)
    {
        CatalogParser.CatalogParser catalogParser = new();
        StudyTree parsedStudyTree = catalogParser.ParseStudyTree();
        Console.WriteLine($"size: {parsedStudyTree.Topics.Count}");
        await syncFsWithDb.Execute(parsedStudyTree);
        // List<TopicEntity> topics = await topicRepository.GetEverything();
    }
}