using ApiTemplate.Config.IoC;

namespace ApiTemplate.Features.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public void Register(WebApplication app)
    {
        app.MapPost("/login", () =>
        {
            return Results.Ok();
        }).AllowAnonymous();
    }
}
