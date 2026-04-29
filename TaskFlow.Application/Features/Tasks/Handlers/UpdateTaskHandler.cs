using MediatR;
using TaskFlow.Application.Common.Exceptions;
using TaskFlow.Application.Features.Tasks.Commands;
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
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task is null || task.UserId != request.UserId)
            throw new NotFoundException("Task", request.Id);

        task.Title = request.Title;
        task.Description = request.Description;
        task.Category = request.Category;
        task.Priority = request.Priority;

        if (request.Status == "Completed" && task.Status != "Completed")
            task.CompletedAt = DateTime.UtcNow;
        else if (request.Status != "Completed")
            task.CompletedAt = null;

        task.Status = request.Status;

        await _taskRepository.UpdateAsync(task);
        return Unit.Value;
    }
}