using MediatR;
using TaskFlow.Domain.Entities;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Application.Features.Tasks.Queries;

namespace TaskFlow.Application.Features.Tasks.Queries;
public class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskItem>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskItem> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task == null)
            throw new KeyNotFoundException($"Task with ID {request.Id} was not found.");

        return task;
    }
}
