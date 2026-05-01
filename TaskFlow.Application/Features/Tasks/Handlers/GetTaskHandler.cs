using MediatR;
using TaskFlow.Application.Common.Exceptions;
using TaskFlow.Application.Features.Tasks.DTOs;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Application.Features.Tasks.Queries;

namespace TaskFlow.Application.Features.Tasks.Queries;

public class GetTaskHandler : IRequestHandler<GetTaskQuery, TaskResponse>
{
    private readonly ITaskRepository _taskRepository;

    public GetTaskHandler(ITaskRepository taskRepository)
    {
        _taskRepository = taskRepository;
    }

    public async Task<TaskResponse> Handle(GetTaskQuery request, CancellationToken cancellationToken)
    {
        var task = await _taskRepository.GetByIdAsync(request.Id);

        if (task is null || task.UserId != request.UserId)
            throw new NotFoundException("Task", request.Id);

        return new TaskResponse
        {
            Id = task.Id,
            Title = task.Title,
            Description = task.Description,
            Category = task.Category,
            Priority = task.Priority,
            Status = task.Status,
            CreatedAt = task.CreatedAt,
            CompletedAt = task.CompletedAt
        };
    }
}
