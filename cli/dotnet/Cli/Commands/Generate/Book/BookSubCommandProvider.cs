namespace Exercises.Cli.Commands.Generate.Book;

using System.CommandLine;
using Exercises.Common;
using Exercises.Logic.Controllers.Generate;

public class BookSubCommandProvider(
        GenerateFromBooks generateFromBooks
        )
{
    public async Task<Command> SetupCommand(Command command)
    {
        return await BookSubCommand(command).ConfigureAwait(false);
    }

    private async Task<Command> BookSubCommand(Command command)
    {
        Command bookCommand = new("book", "Generate from the given book.");
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

        bookCommand.SetAction(async parseResult =>
        {
            GenerateBooksCommandParameters parameters = new(
                   parseResult.GetValue(skillQuestionVolume),
                   parseResult.GetValue(applicationQuestionVolume),
                   parseResult.GetValue(conceptQuestionVolume),
                   parseResult.GetValue(discussionQuestionVolume)
                   );

            await generateFromBooks.Execute(parameters).ConfigureAwait(false);
        });
        command.Add(bookCommand);
        return command;
    }

}