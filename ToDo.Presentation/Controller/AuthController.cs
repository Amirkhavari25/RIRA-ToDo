using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using ToDo.Application.DTOs;
using ToDo.Application.Features.Users.Commands.Login;
using ToDo.Application.Features.Users.Commands.Register;

namespace ToDo.Presentation.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }


        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterCommand command)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data");
            }
            var result = await _mediator.Send(command);
            if (result.Success)
            {
                return Ok(result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest("Invalid data");

            var result = await _mediator.Send(command);

            if (!result.Success)
                return NotFound(result.ErrorMessage);

            return Ok(new { Token = result.Data });
        }
    }
}
