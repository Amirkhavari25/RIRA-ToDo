using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.Features.Tasks.Commands.CreateTask;

namespace ToDo.Presentation.Controller
{
   // [Authorize]
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

            var creatorId = User.FindFirst("sub")?.Value;
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
    }
}
