namespace Exercises.Logic.Repository.Section;

using Common;
using LanguageExt;
using Models;
using static LanguageExt.Prelude;

public partial class SectionRepository
{
    public Either<ExerciseError, SectionEntity> Add(
        SectionEntity input,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Sections.Add(input);
            ctx.SaveChanges();
            return Right(input);
        }
        catch (Exception e)
        {
            return Left<ExerciseError, SectionEntity>(
                new ExerciseError($"Error happened while recording {nameof(SectionEntity)}. Error: {e.Message}"));
        }
    }
}