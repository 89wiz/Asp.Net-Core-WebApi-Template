using ApiTemplate.Config.IoC;
using Med8r;

namespace ApiTemplate.Features.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public void Register(WebApplication app)
    {
        app.MapPost("/login", async (LoginRequest request, IMed8r med8R) =>
        {
            var response = await med8R.Send(request);
            return Results.Ok(response);
        }).AllowAnonymous();
    }
}
