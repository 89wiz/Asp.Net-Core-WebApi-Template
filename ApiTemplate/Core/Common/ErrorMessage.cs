using FluentValidation.Results;

namespace ApiTemplate.Core.Common;

public class ErrorMessage
{
    public required int ErrorCode { get; set; }
    public required string Message { get; set; }
    public List<ErrorMessage> ErrorDetails { get; set; } = [];
}

public static class FluentValidationExtensions
{
    public static ErrorMessage ToErrorMessage(this ValidationResult result, string message = "Erro")
    {
        return new ErrorMessage
        {
            ErrorCode = ErrorCodes.INVALID_ENTITY,
            Message = message,
            ErrorDetails = result.Errors.Select(x => new ErrorMessage { ErrorCode = ErrorCodes.INVALID_ENTITY, Message = x.ErrorMessage }).ToList()
        };
    }
}