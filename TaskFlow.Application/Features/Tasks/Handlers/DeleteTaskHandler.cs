using MediatR;
using TaskFlow.Application.Common.Exceptions;
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
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task is null || task.UserId != request.UserId)
            throw new NotFoundException("Task", request.Id);

        await _taskRepository.DeleteAsync(request.Id);
        return Unit.Value;
    }
}

