using HMS.Application.Features.Doctors.Commands;
using HMS.Application.Features.Doctors.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class DoctorsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetDoctors(
        [FromQuery] int pageNumber = 1, 
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null, 
        [FromQuery] string? sortBy = null, 
        [FromQuery] bool isDescending = false, 
        CancellationToken ct = default)
    {
        var query = new GetDoctorsQuery(pageNumber, pageSize, searchTerm, sortBy, isDescending);
        var result = await mediator.Send(query, ct);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetDoctorById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetDoctorByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateDoctor([FromBody] CreateDoctorCommand command, CancellationToken ct)
        => Ok(await mediator.Send(command, ct));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateDoctor(Guid id, [FromBody] UpdateDoctorCommand command, CancellationToken ct)
    {
        if (id != command.Id) return BadRequest("ID mismatch.");
        return Ok(await mediator.Send(command, ct));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteDoctor(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new DeleteDoctorCommand(id), ct));
}
