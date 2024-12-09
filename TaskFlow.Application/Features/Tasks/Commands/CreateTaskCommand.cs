using MediatR;

namespace TaskFlow.Application.Features.Tasks.Commands;
public class CreateTaskCommand : IRequest<Guid>
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public string Priority { get; set; }
}
