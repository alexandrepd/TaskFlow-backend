using MediatR;
using TaskFlow.Application.Features.Auth.DTOs;

namespace TaskFlow.Application.Features.Auth;

public record GetUsersQuery : IRequest<IEnumerable<UserSummaryResponse>>;
