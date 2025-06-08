using ApiTemplate.Core.Common;
using ApiTemplate.Core.Entities;
using Mediator;
using Microsoft.EntityFrameworkCore;
using Riok.Mapperly.Abstractions;

namespace ApiTemplate.Features.Auth.Register;

public class RegisterRequest : IRequest<ErrorOr<RegisterResponse>>
{
    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set; }
}

public class RegisterResponse
{
    public Guid Id { get; set; }
    public string Username { get; set; }
    public string Email { set; get; }
}

public class RegisterRequestHandler(DbContext context) : IRequestHandler<RegisterRequest, ErrorOr<RegisterResponse>>
{
    public async ValueTask<ErrorOr<RegisterResponse>> Handle(RegisterRequest request, CancellationToken cancellationToken)
    {
        var userSet = context.Set<User>();

        var existingEmail = userSet.CountAsync(x => x.Email == request.Email, cancellationToken);
        if (existingEmail is not null)
            return new ErrorMessage { ErrorCode = ErrorCodes.EMAIL_ALREADY_REGISTERED, Message = "Email already registered" };

        var existingUsername = userSet.CountAsync(x => x.Username == request.Username, cancellationToken);
        if (existingUsername is not null)
            return new ErrorMessage { ErrorCode = ErrorCodes.USERNAME_ALREADY_REGISTERED, Message = "Username already registered" };

        var user = new User
        {
            Id = Guid.CreateVersion7(),
            Username = request.Username,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
        };

        await userSet.AddAsync(user, cancellationToken);

        return user.ToResponse();
    }
}

[Mapper]
public static partial class RegisterMapper
{
    public static partial RegisterResponse ToResponse(this User user);
}