using ApiTemplate.Core.Common;

namespace ApiTemplate.Config.Http;

public static class ResultsExtensions
{
    public static async Task<IResult> ToResult<T>(this ValueTask<ErrorOr<T>> task)
        where T : class
    {
        var response = await task;

        if (response is { HasValue: true })
            return Results.Ok(response.Value);

        var error = response.Error;

        return error switch
        {
            { ErrorCode: ErrorCodes.INVALID_ENTITY } => Results.UnprocessableEntity(error),
            
            _ => Results.BadRequest(response.Error),
        };
    }
}
