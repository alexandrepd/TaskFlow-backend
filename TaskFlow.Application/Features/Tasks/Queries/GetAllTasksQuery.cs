using MediatR;
using TaskFlow.Domain.Entities;
using System.Collections.Generic;

namespace TaskFlow.Application.Features.Tasks.Queries;

public class GetAllTasksQuery : IRequest<IEnumerable<TaskItem>>
{
}