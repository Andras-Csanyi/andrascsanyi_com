namespace Exercises.Cli;

using System.CommandLine;
using Exercises.Cli.SubCommands;
using Exercises.Logic.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static int Main(string[] args)
    {
        RootCommand rootCommand = new("Exercises Command Line Tool.");
        rootCommand = Generate.SetupCommand(rootCommand);
        var builder = Host.CreateApplicationBuilder(args);
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        builder.Services.AddDbContext<ExercisesContext>(options => options.UseNpgsql(
                    builder.Configuration.GetConnectionString("DefaultConnection"))
                    );

        return rootCommand.Parse(args).Invoke();
    }
}