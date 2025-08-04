namespace Logic.Controllers.Generate;

public class GenerateFromBooks
{
    public void Execute()
    {
        CatalogParser.CatalogParser catalogParser = new();
        catalogParser.ParseStudyTree();
    }
}
