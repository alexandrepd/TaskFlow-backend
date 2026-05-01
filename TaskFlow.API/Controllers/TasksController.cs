using System.Net;
using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Tasks.Commands;
using TaskFlow.Application.Features.Tasks.DTOs;
using TaskFlow.Application.Features.Tasks.Queries;

namespace TaskFlow.API.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetCurrentUserId()
        {
            var claim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return Guid.TryParse(claim, out var userId)
                ? userId
                : throw new UnauthorizedAccessException("User identity is invalid.");
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskResponse>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _mediator.Send(new GetAllTasksQuery { UserId = GetCurrentUserId() });
            return Ok(tasks);
        }

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskResponse), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _mediator.Send(new GetTaskQuery { Id = id, UserId = GetCurrentUserId() });
            return Ok(task);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            command.UserId = GetCurrentUserId();
            var taskId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTask), new { id = taskId }, new { id = taskId });
        }

        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest(new { Message = "Task ID mismatch." });

            command.UserId = GetCurrentUserId();
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            await _mediator.Send(new DeleteTaskCommand { Id = id, UserId = GetCurrentUserId() });
            return NoContent();
        }
    }
}
