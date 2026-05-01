using MediatR;

namespace TaskFlow.Application.Features.Auth;

public record RegisterUserCommand(string Username, string Password) : IRequest<string>;
