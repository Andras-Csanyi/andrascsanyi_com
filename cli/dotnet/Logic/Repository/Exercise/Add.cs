namespace Exercises.Logic.Repository.Exercise;

using Common;
using LanguageExt;
using Models;

public partial class ExerciseRepository
{
    public Either<ExerciseError, ExerciseEntity> Add(
        ExerciseEntity mappedInput,
        ExercisesContext ctx
    )
    {
        try
        {
            ctx.Exercises.Add(mappedInput);
            ctx.SaveChanges();
            return Right(mappedInput);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}