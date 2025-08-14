namespace Exercises.Cli.Commands.Generate.Book;

using System.CommandLine;
using Common;
using Logic.Scenarios.Generate;

public class BookSubCommandProvider(
    GenerateFromBooksScenario generateFromBooksScenario
)
{
    public Command SetupCommand(
        Command command
    ) => BookSubCommand(command);

    private Command BookSubCommand(
        Command command
    )
    {
        Command bookCommand = new("book", "Generate from the given book.");
        Option<string> books = new("--books")
        {
            Description = "The books to generate from by their reference.", Required = true,
        };
        bookCommand.Add(books);
        Option<int> skillQuestionVolume = new("--skill")
        {
            Description = "How many skill questions will be included in the result test.",
        };
        bookCommand.Add(skillQuestionVolume);

        Option<int> applicationQuestionVolume = new("--app")
        {
            Description = "How many application questions will be included in the result test.",
        };
        bookCommand.Add(applicationQuestionVolume);

        Option<int> conceptQuestionVolume = new("--concept")
        {
            Description = "How many concept questions will be included in the result test.",
        };
        bookCommand.Add(conceptQuestionVolume);

        Option<int> discussionQuestionVolume = new("--discussion")
        {
            Description = "How many discussion questions will be included in the result test.",
        };
        bookCommand.Add(discussionQuestionVolume);

        bookCommand.SetAction(parseResult =>
            {
                GenerateFromBooksScenarioParameters parameters = new(
                    parseResult.GetValue(skillQuestionVolume),
                    parseResult.GetValue(applicationQuestionVolume),
                    parseResult.GetValue(conceptQuestionVolume),
                    parseResult.GetValue(discussionQuestionVolume),
                    parseResult.GetValue(books)!
                );

                generateFromBooksScenario.Execute(parameters);
            }
        );
        command.Add(bookCommand);
        return command;
    }
}