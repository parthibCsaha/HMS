using HMS.Application.Features.Departments.Commands;
using HMS.Application.Features.Departments.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DepartmentsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        CancellationToken ct = default
    ) => Ok(await mediator.Send(new GetDepartmentsQuery(pageNumber, pageSize, searchTerm), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetDepartmentByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
        [FromBody] CreateDepartmentCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateDepartmentCommand cmd,
        CancellationToken ct
    )
    {
        if (id != cmd.Id)
            return BadRequest("ID mismatch.");
        return Ok(await mediator.Send(cmd, ct));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new DeleteDepartmentCommand(id), ct));
}
