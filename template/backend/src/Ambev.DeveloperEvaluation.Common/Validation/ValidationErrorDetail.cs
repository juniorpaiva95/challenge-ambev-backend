using FluentValidation.Results;

namespace Ambev.DeveloperEvaluation.Common.Validation;

public class ValidationErrorDetail
{
    public string Error { get; init; } = string.Empty;
    public string Detail { get; init; } = string.Empty;
    public string PropertyName { get; init; } = string.Empty;
    public object? AttemptedValue { get; init; }
    public string? ErrorCode { get; init; }
    public string? Severity { get; init; }

    public static explicit operator ValidationErrorDetail(ValidationFailure validationFailure)
    {
        return new ValidationErrorDetail
        {
            Detail = validationFailure.ErrorMessage,
            Error = validationFailure.ErrorCode,
            PropertyName = validationFailure.PropertyName,
            AttemptedValue = validationFailure.AttemptedValue,
            ErrorCode = validationFailure.ErrorCode,
            Severity = validationFailure.Severity.ToString()
        };
    }
}
