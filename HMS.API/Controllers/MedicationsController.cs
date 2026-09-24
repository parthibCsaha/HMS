using HMS.Application.Features.Medications.Commands;
using HMS.Application.Features.Medications.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class MedicationsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] string? sortBy = null,
        CancellationToken ct = default
    ) =>
        Ok(
            await mediator.Send(
                new GetMedicationsQuery(pageNumber, pageSize, searchTerm, sortBy),
                ct
            )
        );

    [HttpGet("low-stock")]
    [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> GetLowStock(CancellationToken ct) =>
        Ok(await mediator.Send(new GetLowStockMedicationsQuery(), ct));

    [HttpPost]
    [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> Create(
        [FromBody] CreateMedicationCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Admin,Pharmacist")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateMedicationCommand cmd,
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
        Ok(await mediator.Send(new DeleteMedicationCommand(id), ct));
}
