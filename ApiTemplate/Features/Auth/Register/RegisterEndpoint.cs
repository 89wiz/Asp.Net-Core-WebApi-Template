using ApiTemplate.Config.Http;
using ApiTemplate.Config.IoC;
using ApiTemplate.Core.Common;
using Mediator;

namespace ApiTemplate.Features.Auth.Register;

public class RegisterEndpoint : IEndpoint
{
    public void Register(WebApplication app)
    {
        app.MapPost("register", static (ISender sender, RegisterRequest request) => sender.Send(request).ToResult())
            .Produces<RegisterResponse>()
            .Produces<ErrorMessage>(400);
    }
}