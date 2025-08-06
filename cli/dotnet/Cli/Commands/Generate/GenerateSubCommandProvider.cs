namespace Exercises.Cli.Commands.Generate;

using System.CommandLine;
using Exercises.Cli.Commands.Generate.Book;

public class GenerateSubCommandProvider(
        BookSubCommandProvider bookSubCommandProvider
        )
{
    public async Task<RootCommand> SetupCommand(RootCommand rootCommand)
    {
        Command generateCommand = new("generate", "Generates exercises.");
        generateCommand = await bookSubCommandProvider.SetupCommand(generateCommand);

        rootCommand.Subcommands.Add(generateCommand);
        return rootCommand;
    }

}