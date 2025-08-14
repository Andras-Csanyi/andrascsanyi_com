namespace Exercises.Logic.Generators.LaTeX;

using System.Collections.Immutable;
using Common;
using Repository.Models;
using Scriban;

public static class LaTeXGenerator
{
    public static Either<ExerciseError, Unit> Execute(ImmutableList<ExerciseEntity> entities) =>
        from parsedTemplate in ParseTemplateFile()
        from compiledTemplate in CompiledTemplate(parsedTemplate)
        from processedTemplate in ProcessTemplate(compiledTemplate, entities)
        from targetFile in CreateTargetFile()
        from _ in WriteFile(targetFile, processedTemplate)
        select Unit.Default;

    private static Either<ExerciseError, Unit> WriteFile(string targetFile, string processedTemplate)
    {
        try
        {
            File.WriteAllText(targetFile, processedTemplate);
            return Unit.Default;
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }

    private static Either<ExerciseError, string> ProcessTemplate(
        Template template,
        ImmutableList<ExerciseEntity> entities
    )
    {
        try
        {
            string? result = template.Render(new { Data = entities, });
            return Right(result);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }

    private static Either<ExerciseError, string> CreateTargetFile()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string targetPath = Path.Combine(currentDirectory, "target.tex");
            return Right(targetPath);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }

    private static Either<ExerciseError, Template> CompiledTemplate(
        string parsedTemplate
    )
    {
        try
        {
            Template? compiledTemplate = Template.Parse(parsedTemplate);
            return Right(compiledTemplate);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }

    private static Either<ExerciseError, string> ParseTemplateFile()
    {
        try
        {
            string currentDirectory = Directory.GetCurrentDirectory();
            string templatePath = Path.Combine(currentDirectory, "Logic/Generators/LaTeX/exercise.sbn");
            string template = File.ReadAllText(templatePath);
            return Right(template);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}