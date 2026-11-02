using HMS.Application.Features.Auth.Commands.ChangePassword;
using HMS.Application.Features.Auth.Commands.Login;
using HMS.Application.Features.Auth.Commands.Logout;
using HMS.Application.Features.Auth.Commands.RefreshToken;
using HMS.Application.Features.Auth.Commands.Register;
using HMS.Application.Features.Auth.Queries.GetCurrentUser;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[EnableRateLimiting("auth")]
public class AuthController(ISender mediator) : ControllerBase
{
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterCommand command,
        CancellationToken ct
    ) => Ok(await mediator.Send(command, ct));

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginCommand command,
        CancellationToken ct
    ) => Ok(await mediator.Send(command, ct));

    [AllowAnonymous]
    [HttpPost("refresh-token")]
    public async Task<IActionResult> RefreshToken(
        [FromBody] RefreshTokenCommand command,
        CancellationToken ct
    ) => Ok(await mediator.Send(command, ct));

    [Authorize]
    [HttpPost("logout")]
    public async Task<IActionResult> Logout(CancellationToken ct) =>
        Ok(await mediator.Send(new LogoutCommand(), ct));

    [Authorize]
    [HttpPost("change-password")]
    public async Task<IActionResult> ChangePassword(
        [FromBody] ChangePasswordCommand command,
        CancellationToken ct
    ) => Ok(await mediator.Send(command, ct));

    [Authorize]
    [HttpGet("me")]
    public async Task<IActionResult> GetCurrentUser(CancellationToken ct) =>
        Ok(await mediator.Send(new GetCurrentUserQuery(), ct));
}
