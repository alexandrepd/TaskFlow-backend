using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Auth;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
    {
        var token = await _mediator.Send(command);
        return Ok(new { token });
    }
}