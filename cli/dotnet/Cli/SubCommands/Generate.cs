using System.CommandLine;
using Logic.Controllers.Generate;

namespace Cli.SubCommands;

public static class Generate
{
    public static RootCommand SetupCommand(RootCommand rootCommand)
    {
        Command generateCommand = new("generate", "Generates exercises.");
        Command bookCommand = new("book", "Generate from the given book.");
        bookCommand.SetAction(parseResult =>
        {
            GenerateFromBooks generateFromBooks = new();
            generateFromBooks.Execute();
        });
        generateCommand.Subcommands.Add(bookCommand);
        Command topicCommand = new("topic", "Generate from the books in the given topic.");
        topicCommand.SetAction(parseResult => Console.WriteLine("Topic is parsed"));
        generateCommand.Subcommands.Add(topicCommand);
        rootCommand.Subcommands.Add(generateCommand);
        return rootCommand;
    }
}