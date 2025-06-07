using Med8r;

namespace ApiTemplate.Features.Auth.Login;

public class LoginRequest : IRequest<LoginResponse>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

public class LoginResponse
{
    public string Token { get; set; }
}

public class LoginRequestHandler : IRequestHandler<LoginRequest, LoginResponse>
{
    public Task<LoginResponse> Handle(LoginRequest request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
