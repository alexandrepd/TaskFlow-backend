using MediatR;
using TaskFlow.Application.Features.Tasks.DTOs;

namespace TaskFlow.Application.Features.Tasks.Queries;

public class GetTaskQuery : IRequest<TaskResponse>
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
}
