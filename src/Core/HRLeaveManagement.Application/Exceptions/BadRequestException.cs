
using FluentValidation.Results;

namespace src.Core.Exceptions;

public class BadRequestException : Exception
{
    public List<string> ValidationErrors { get; set; }
    public BadRequestException(string message) : base(message)
    {
    }
    public BadRequestException(string message, ValidationResult validationResult) : base(message)
    {
        ValidationErrors = [];

        foreach (var err in validationResult.Errors)
        {
            ValidationErrors.Add(err.ErrorMessage);
        }
    }
}
