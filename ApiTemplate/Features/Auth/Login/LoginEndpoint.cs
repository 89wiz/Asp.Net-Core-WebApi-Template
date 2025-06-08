using ApiTemplate.Config.Http;
using ApiTemplate.Config.IoC;
using ApiTemplate.Core.Common;
using Mediator;

namespace ApiTemplate.Features.Auth.Login;

public class LoginEndpoint : IEndpoint
{
    public void Register(WebApplication app)
    {
        app.MapPost("login", static (LoginRequest request, ISender sender) => sender.Send(request).ToResult())
            .Produces<LoginResponse>(200)
            .Produces<ErrorMessage>(400)
            .AllowAnonymous();
    }
}
