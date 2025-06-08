using ApiTemplate.Core.Common;
using Mediator;

namespace ApiTemplate.Features.Auth.Login;

public class LoginRequest : IRequest<ErrorOr<LoginResponse>>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
}

public class LoginRequestHandler : IRequestHandler<LoginRequest, ErrorOr<LoginResponse>>
{
    public ValueTask<ErrorOr<LoginResponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
