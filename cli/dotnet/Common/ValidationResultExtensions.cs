namespace Exercises.Common;

using FluentValidation.Results;

public static class ValidationResultExtensions
{
    public static string ToErrorString(this ValidationResult validationResult) =>
        string.Join(";", validationResult.Errors.Select(vf => vf.ErrorMessage).ToList());
}