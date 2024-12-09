using MediatR;
using TaskFlow.Domain.Entities;

namespace TaskFlow.Application.Features.Tasks.Queries;

public class GetTaskQuery : IRequest<TaskItem>
{
    public Guid Id { get; set; }
}
