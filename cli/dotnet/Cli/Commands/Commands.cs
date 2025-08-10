namespace Exercises.Cli.Commands;

using System.CommandLine;
using Generate;

public class Root(
    GenerateSubCommandProvider generateSubCommandProvider
)
{
    public RootCommand BuildCli()
    {
        RootCommand rootCommand = new("Exercises Command Line Tool.");
        rootCommand = generateSubCommandProvider.SetupCommand(rootCommand);
        return rootCommand;
    }
}