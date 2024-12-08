using MediatR;

namespace TaskFlow.Application.Features.Auth;

public class AuthenticateUserCommand : IRequest<string>
{
    public string Username { get; set; }
    public string Password { get; set; }
}

