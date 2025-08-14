namespace Exercises.Logic.Repository.Exercise;

using Common;
using Microsoft.EntityFrameworkCore;
using Models;

public partial class ExerciseRepository
{
    public Either<ExerciseError, List<ExerciseEntity>> FindByBookReferences(string[] bookReferences)
    {
        using ExercisesContext ctx = new(dbContextOptions);
        try
        {
            List<ExerciseEntity> exercises = ctx.Exercises.Include(i => i.Book)
                .Where(w => bookReferences.Contains(w.Book.Reference))
                .ToList();
            return Right(exercises);
        }
        catch (Exception e)
        {
            return Left(new ExerciseError(e.Message));
        }
    }
}