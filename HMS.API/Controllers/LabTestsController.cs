using HMS.Application.Features.LabTests.Commands;
using HMS.Application.Features.LabTests.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/lab-tests")]
[Authorize]
public class LabTestsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null,
        CancellationToken ct = default
    ) =>
        Ok(await mediator.Send(new GetLabTestsQuery(pageNumber, pageSize, searchTerm, sortBy), ct));

    [HttpPost]
    [Authorize(Roles = "Admin,LabTechnician")]
    public async Task<IActionResult> Create(
        [FromBody] CreateLabTestCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,LabTechnician")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateLabTestCommand cmd,
        CancellationToken ct
    )
    {
        if (id != cmd.Id)
            return BadRequest("ID mismatch.");
        return Ok(await mediator.Send(cmd, ct));
    }
}
