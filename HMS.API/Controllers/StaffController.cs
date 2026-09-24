using HMS.Application.Features.Staff.Commands;
using HMS.Application.Features.Staff.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class StaffController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetStaff(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool isDescending = false,
        CancellationToken ct = default
    ) =>
        Ok(
            await mediator.Send(
                new GetStaffQuery(pageNumber, pageSize, searchTerm, sortBy, isDescending),
                ct
            )
        );

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetStaffById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetStaffByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateStaff(
        [FromBody] CreateStaffCommand command,
        CancellationToken ct
    ) => Ok(await mediator.Send(command, ct));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateStaff(
        Guid id,
        [FromBody] UpdateStaffCommand command,
        CancellationToken ct
    )
    {
        if (id != command.Id)
            return BadRequest("ID mismatch.");
        return Ok(await mediator.Send(command, ct));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteStaff(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new DeleteStaffCommand(id), ct));
}
