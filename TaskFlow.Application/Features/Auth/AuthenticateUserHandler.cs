using MediatR;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Auth;

public class AuthenticateUserHandler : IRequestHandler<AuthenticateUserCommand, string>
{
    private readonly IJwtTokenService _jwtTokenService;

    public AuthenticateUserHandler(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public async Task<string> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {

        if (request.Username == "adminTask" && request.Password == "passwordTask")
        {
            return _jwtTokenService.GenerateToken(request.Username, "Admin");
        }

        throw new UnauthorizedAccessException("Invalid credentials");
    }
}

