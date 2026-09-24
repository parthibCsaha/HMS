using HMS.Application.Features.Patients.Commands;
using HMS.Application.Features.Patients.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class PatientsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetPatients(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null,
        [FromQuery] bool isDescending = false,
        CancellationToken ct = default
    ) =>
        Ok(
            await mediator.Send(
                new GetPatientsQuery(pageNumber, pageSize, searchTerm, sortBy, isDescending),
                ct
            )
        );

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetPatientById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetPatientByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Admin,Receptionist,Doctor,Nurse")]
    public async Task<IActionResult> CreatePatient(
        [FromBody] CreatePatientCommand command,
        CancellationToken ct
    ) =>
        CreatedAtAction(
            nameof(GetPatientById),
            new { id = (await mediator.Send(command, ct)).Data },
            null
        );

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Receptionist,Doctor,Nurse")]
    public async Task<IActionResult> UpdatePatient(
        Guid id,
        [FromBody] UpdatePatientCommand command,
        CancellationToken ct
    )
    {
        if (id != command.Id)
            return BadRequest("ID mismatch.");
        return Ok(await mediator.Send(command, ct));
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeletePatient(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new DeletePatientCommand(id), ct));
}
