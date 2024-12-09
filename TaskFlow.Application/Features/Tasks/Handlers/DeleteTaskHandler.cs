using MediatR;
using TaskFlow.Application.Features.Tasks.Commands;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Handlers;

public class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public DeleteTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        // Fetch the task from the repository
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
            throw new KeyNotFoundException("Task not found.");

        // Delete the task
        await _taskRepository.DeleteAsync(request.Id);

        return Unit.Value; // Signal that the command has been handled
    }
}

