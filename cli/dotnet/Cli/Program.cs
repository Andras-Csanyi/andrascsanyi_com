namespace Exercises.Cli;

using System.CommandLine;
using System.Threading.Tasks;
using Exercises.Cli.Commands;
using Exercises.Cli.Commands.Generate;
using Exercises.Cli.Commands.Generate.Book;
using Exercises.Logic.Controllers.Generate;
using Exercises.Logic.Repository;
using Exercises.Logic.Repository.Topic;
using Exercises.Logic.Sync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static async Task<int> Main(string[] args)
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .AddEnvironmentVariables();
        builder.Services.AddDbContext<ExercisesContext>(options =>
                {
                    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                    options.EnableDetailedErrors();
                });
        // commands
        builder.Services.AddTransient<Root>();
        builder.Services.AddTransient<BookSubCommandProvider>();
        builder.Services.AddTransient<GenerateSubCommandProvider>();

        // controllers
        builder.Services.AddTransient<GenerateFromBooks>();

        // repositories
        builder.Services.AddTransient<SyncFsWithDb>();
        builder.Services.AddTransient<TopicRepository>();

        using IHost host = builder.Build();
        IServiceProvider serviceProvider = host.Services;

        using ExercisesContext dbContext = serviceProvider.GetRequiredService<ExercisesContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        Root rootCommand = serviceProvider.GetRequiredService<Root>();
        RootCommand rc = await rootCommand.BuildCli();

        return rc.Parse(args).Invoke();
    }
}