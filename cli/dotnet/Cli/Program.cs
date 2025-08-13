namespace Exercises.Cli;

using System.CommandLine;
using Commands;
using Commands.Generate;
using Commands.Generate.Book;
using Logic.Controllers.Generate;
using Logic.Repository;
using Logic.Repository.Book;
using Logic.Repository.Chapter;
using Logic.Repository.Exercise;
using Logic.Repository.Section;
using Logic.Repository.Topic;
using Logic.Scenarios.Book.AddNew;
using Logic.Scenarios.Book.Find;
using Logic.Scenarios.Book.UpdateBook;
using Logic.Scenarios.Chapter.Add;
using Logic.Scenarios.Chapter.Find;
using Logic.Scenarios.Chapter.Update;
using Logic.Scenarios.Exercise.UpdateOrInsert;
using Logic.Scenarios.Section.Add;
using Logic.Scenarios.Section.Find;
using Logic.Scenarios.Topic.Add;
using Logic.Scenarios.Topic.Find;
using Logic.Sync;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

internal class Program
{
    private static int Main(
        string[] args
    )
    {
        HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
        builder.Configuration
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables();
        builder.Services.AddDbContext<ExercisesContext>(options =>
            {
                options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
                options.EnableDetailedErrors();
                options.EnableSensitiveDataLogging();
            }
        );
        builder.Services.AddTransient<FindTopicByNameAndReferenceScenario>();
        builder.Services.AddTransient<FindBookByTopicIdAndReferenceScenario>();
        builder.Services.AddTransient<AddNewTopicScenario>();
        builder.Services.AddTransient<AddNewTopicScenarioInputValidator>();
        builder.Services.AddTransient<UpdateBookScenario>();
        builder.Services.AddTransient<UpdateBookScenarioInputValidator>();
        builder.Services.AddTransient<FindChapterByBookIdAndReferenceScenario>();
        builder.Services.AddTransient<AddNewChapterScenario>();
        builder.Services.AddTransient<AddNewChapterScenarioInputValidator>();
        builder.Services.AddTransient<UpdateChapterScenario>();
        builder.Services.AddTransient<UpdateChapterScenarioInputValidator>();
        builder.Services.AddTransient<FindSectionByChapterIdAndSectionNumberScenario>();
        builder.Services.AddTransient<AddNewSectionScenario>();
        builder.Services.AddTransient<AddNewSectionScenarioInputValidation>();
        builder.Services.AddTransient<GetAllTopicsScenario>();
        builder.Services.AddTransient<UpdateOrInsertExerciseScenario>();
        builder.Services.AddTransient<UpdateExerciseScenarioInputValidator>();
        builder.Services.AddTransient<AddExerciseScenarioInputValidator>();
        builder.Services.AddTransient<FindTopicByNameScenario>();
        builder.Services.AddTransient<FindBookByReferenceScenario>();

        // Add new book scenario
        builder.Services.AddTransient<AddNewBookByTopicIdAndParsedBook>();
        builder.Services.AddTransient<AddNewBookScenarioInputValidator>();

        // cli commands
        builder.Services.AddTransient<Root>();
        builder.Services.AddTransient<BookSubCommandProvider>();
        builder.Services.AddTransient<GenerateSubCommandProvider>();

        // controllers
        builder.Services.AddTransient<GenerateFromBooks>();

        // repositories
        builder.Services.AddTransient<BookRepository>();
        builder.Services.AddTransient<SyncFsWithDb>();
        builder.Services.AddTransient<TopicRepository>();
        builder.Services.AddTransient<ChapterRepository>();
        builder.Services.AddTransient<SectionRepository>();
        builder.Services.AddTransient<ExerciseRepository>();

        using IHost host = builder.Build();
        IServiceProvider serviceProvider = host.Services;

        using ExercisesContext dbContext = serviceProvider.GetRequiredService<ExercisesContext>();
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();

        Root rootCommand = serviceProvider.GetRequiredService<Root>();
        RootCommand rc = rootCommand.BuildCli();

        return rc.Parse(args).Invoke();
    }
}