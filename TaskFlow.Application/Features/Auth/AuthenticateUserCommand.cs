using MediatR;

namespace TaskFlow.Application.Features.Auth;

public class AuthenticateUserCommand : IRequest<string>
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

