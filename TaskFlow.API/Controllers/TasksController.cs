using System.Net;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Tasks.Commands;
using TaskFlow.Application.Features.Tasks.Queries;
using TaskFlow.Domain.Entities;

namespace TaskFlow.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TasksController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TasksController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Obtém todas as tarefas.
        /// </summary>
        /// <returns>Lista de tarefas</returns>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TaskItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllTasks()
        {
            var tasks = await _mediator.Send(new GetAllTasksQuery());
            return Ok(tasks);
        }

        /// <summary>
        /// Obtém uma tarefa específica pelo ID.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <returns>Tarefa encontrada</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TaskItem), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetTask(Guid id)
        {
            var task = await _mediator.Send(new GetTaskQuery { Id = id });
            if (task == null)
                return NotFound(new { Message = "Task not found." });
            return Ok(task);
        }

        /// <summary>
        /// Cria uma nova tarefa.
        /// </summary>
        /// <param name="command">Comando para criar a tarefa</param>
        /// <returns>ID da tarefa criada</returns>
        [HttpPost]
        [ProducesResponseType(typeof(Guid), (int)HttpStatusCode.Created)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            if (command == null)
                return BadRequest("Invalid task data.");

            var taskId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTask), new { id = taskId }, null);
        }

        /// <summary>
        /// Atualiza uma tarefa existente.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <param name="command">Comando para atualizar a tarefa</param>
        /// <returns>Status da atualização</returns>
        [HttpPut("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> UpdateTask(Guid id, [FromBody] UpdateTaskCommand command)
        {
            if (id != command.Id)
                return BadRequest("Task ID mismatch.");

            var task = await _mediator.Send(new GetTaskQuery { Id = id });
            if (task == null)
                return NotFound(new { Message = "Task not found." });

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Exclui uma tarefa existente.
        /// </summary>
        /// <param name="id">ID da tarefa</param>
        /// <returns>Status da exclusão</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var task = await _mediator.Send(new GetTaskQuery { Id = id });
            if (task == null)
                return NotFound(new { Message = "Task not found." });

            await _mediator.Send(new DeleteTaskCommand { Id = id });
            return NoContent();
        }
    }
}
