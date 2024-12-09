using MediatR;

namespace TaskFlow.Application.Features.Tasks.Commands;
public class DeleteTaskCommand : IRequest
{
    public Guid Id { get; set; }
}
