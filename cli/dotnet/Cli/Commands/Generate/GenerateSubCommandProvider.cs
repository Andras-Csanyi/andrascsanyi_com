namespace Exercises.Cli.Commands.Generate;

using System.CommandLine;
using Book;

public class GenerateSubCommandProvider(
    BookSubCommandProvider bookSubCommandProvider
)
{
    public RootCommand SetupCommand(
        RootCommand rootCommand
    )
    {
        Command generateCommand = new("generate", "Generates exercises.");
        generateCommand = bookSubCommandProvider.SetupCommand(generateCommand);

        rootCommand.Subcommands.Add(generateCommand);
        return rootCommand;
    }
}