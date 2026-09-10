using HMS.Application.Features.Beds.Commands;
using HMS.Application.Features.Beds.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BedsController(ISender mediator) : ControllerBase
{
    [HttpGet("ward/{wardId:guid}")]
    public async Task<IActionResult> GetByWard(Guid wardId, CancellationToken ct)
    {
        try
        {
            var result = await mediator.Send(new GetBedsByWardQuery(wardId), ct);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetBedByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateBedCommand cmd, CancellationToken ct)
        => Ok(await mediator.Send(cmd, ct));

    [HttpPut("{id:guid}/status")]
    [Authorize(Roles = "Admin,Nurse")]
    public async Task<IActionResult> UpdateStatus(Guid id, [FromBody] ChangeBedStatusCommand cmd, CancellationToken ct)
        => Ok(await mediator.Send(cmd with { Id = id }, ct));

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new DeleteBedCommand(id), ct));
}
