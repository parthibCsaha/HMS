using HMS.Application.Features.SystemSettings.Commands;
using HMS.Application.Features.SystemSettings.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/system-settings")]
[Authorize(Roles = "Admin")]
public class SystemSettingsController(ISender mediator) : ControllerBase
{
    [HttpGet("{key}")]
    public async Task<IActionResult> GetByKey(string key, CancellationToken ct) =>
        Ok(await mediator.Send(new GetSettingByKeyQuery(key), ct));

    [HttpGet("category/{category}")]
    public async Task<IActionResult> GetByCategory(string category, CancellationToken ct) =>
        Ok(await mediator.Send(new GetSettingsByCategoryQuery(category), ct));

    [HttpPost]
    public async Task<IActionResult> Upsert(
        [FromBody] UpsertSettingCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));
}
