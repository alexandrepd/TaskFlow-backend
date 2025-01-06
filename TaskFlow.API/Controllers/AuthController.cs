using MediatR;
using Microsoft.AspNetCore.Mvc;
using TaskFlow.Application.Features.Auth;

namespace TaskFlow.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<AuthController> _logger;

    public AuthController(IMediator mediator, ILogger<AuthController> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] AuthenticateUserCommand command)
    {
        try
        {
            var token = await _mediator.Send(command);
            return Ok(token);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized("Invalid credentials.");
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An unexpected error occurred during login.");
            return StatusCode(500, "An unexpected error occurred.");
        }
    }

}