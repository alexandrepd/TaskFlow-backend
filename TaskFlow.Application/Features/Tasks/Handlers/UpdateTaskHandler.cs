using MediatR;
using TaskFlow.Application.Features.Tasks.Commands;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;

namespace TaskFlow.Application.Features.Tasks.Handlers;

public class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand>
{
    private readonly ITaskRepository _taskRepository;

    public UpdateTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<Unit> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        // Fetch the task from the repository
        var task = await _taskRepository.GetByIdAsync(request.Id);
        if (task == null)
            throw new KeyNotFoundException("Task not found.");

        // Update task properties
        task.Title = request.Title;
        task.Description = request.Description;
        task.Category = request.Category;
        task.Priority = request.Priority;
        task.Status = request.Status;
        task.CreatedAt = DateTime.UtcNow;

        // Update the task in the repository
        await _taskRepository.UpdateAsync(task);

        return Unit.Value; // Signal that the command has been handled
    }
}