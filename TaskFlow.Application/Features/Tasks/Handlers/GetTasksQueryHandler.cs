using MediatR;
using TaskFlow.Application.Features.Tasks.DTOs;
using TaskFlow.Domain.Interfaces;
using TaskFlow.Application.Features.Tasks.Queries;

namespace TaskFlow.Application.Features.Tasks.Queries;

public class GetTasksQueryHandler : IRequestHandler<GetAllTasksQuery, IEnumerable<TaskResponse>>
{
    private readonly ITaskRepository _repository;

    public GetTasksQueryHandler(ITaskRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TaskResponse>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        var tasks = await _repository.GetAllByUserIdAsync(request.UserId);
        return tasks.Select(t => new TaskResponse
        {
            Id = t.Id,
            Title = t.Title,
            Description = t.Description,
            Category = t.Category,
            Priority = t.Priority,
            Status = t.Status,
            CreatedAt = t.CreatedAt,
            CompletedAt = t.CompletedAt
        });
    }
}

