using MediatR;

namespace TaskFlow.Application.Features.Tasks.Commands;

public class UpdateTaskCommand : IRequest
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Priority { get; set; }
    public string Status { get; set; }
}