using MediatR;
using TaskFlow.Application.Features.Auth.DTOs;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Auth;

public class GetUsersHandler : IRequestHandler<GetUsersQuery, IEnumerable<UserSummaryResponse>>
{
    private readonly IUserRepository _userRepository;

    public GetUsersHandler(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<UserSummaryResponse>> Handle(GetUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => new UserSummaryResponse(u.Id, u.Username, u.Role));
    }
}
