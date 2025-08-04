using System.CommandLine;
using Cli.SubCommands;

namespace Cli;

internal class Program
{
    private static int Main(string[] args)
    {
        RootCommand rootCommand = new("Exercises Command Line Tool.");
        rootCommand = Generate.SetupCommand(rootCommand);
        return rootCommand.Parse(args).Invoke();
    }
}
