using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ToDo.Application.Features.Tasks.Commands.CreateTask;
using ToDo.Application.Features.Tasks.Commands.DeleteTask;
using ToDo.Application.Features.Tasks.Commands.UpdateTask;
using ToDo.Application.Features.Tasks.Queries.GetAllTasks;
using ToDo.Application.Features.Tasks.Queries.GetById;
using ToDo.Domain.Entities;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ToDo.Presentation.Controller
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly IMediator _mediator;
        public TaskController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskCommand command)
        {
            if (command == null || !ModelState.IsValid)
            {
                return BadRequest("Invalid Data");
            }
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (creatorId == null)
            {
                return Unauthorized("Invalid User request");
            }
            command.Creator = Guid.Parse(creatorId);
            var result = await _mediator.Send(command);

            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            var creatorId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (creatorId == null)
            {
                return Unauthorized("Invalid User request,Please try to login");
            }
            var result = await _mediator.Send(new GetAllTaskQuery(creatorId));
            if (result.Success)
            {
                return Ok(result);
            }
            else if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = result.ErrorMessage });
            }
            else if (result.ErrorMessage.Contains("validation", StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { message = result.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTaskById(Guid id)
        {
            var result = await _mediator.Send(new GetTaskByIdQuery(id));

            if (result.Success)
            {
                if (result.Data == null)
                {
                    return NotFound("Task not found");
                }
                return Ok(result);
            }
            else if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = result.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTask(Guid id, UpdateTaskCommand command)
        {
            if (id != command.Id)
            {
                return BadRequest("Task Id mismatch");
            }
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return NoContent();
            }
            else if (result.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = result.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = result.ErrorMessage });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTask(Guid id)
        {
            var res = await _mediator.Send(new DeleteTaskCommand(id));
            if (res.Success)
            {
                return NoContent();
            }
            else if (res.ErrorMessage.Contains("not found", StringComparison.OrdinalIgnoreCase))
            {
                return NotFound(new { message = res.ErrorMessage });
            }
            else
            {
                return StatusCode(500, new { message = res.ErrorMessage });
            }
        }
    }
}
