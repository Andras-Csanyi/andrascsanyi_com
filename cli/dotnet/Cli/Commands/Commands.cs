namespace Exercises.Cli.Commands;

using System.CommandLine;
using Exercises.Cli.Commands.Generate;

public class Root(
        GenerateSubCommandProvider generateSubCommandProvider
        )
{
    public async Task<RootCommand> BuildCli()
    {
        RootCommand rootCommand = new("Exercises Command Line Tool.");
        rootCommand = await generateSubCommandProvider.SetupCommand(rootCommand);
        return rootCommand;
    }
}