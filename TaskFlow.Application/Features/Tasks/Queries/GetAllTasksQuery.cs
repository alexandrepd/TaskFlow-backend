using MediatR;
using TaskFlow.Application.Features.Tasks.DTOs;

namespace TaskFlow.Application.Features.Tasks.Queries;

public class GetAllTasksQuery : IRequest<IEnumerable<TaskResponse>>
{
    public Guid UserId { get; set; }
}