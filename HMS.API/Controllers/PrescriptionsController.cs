using HMS.Application.Features.Prescriptions.Commands;
using HMS.Application.Features.Prescriptions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Doctor,Pharmacist")]
public class PrescriptionsController(ISender mediator) : ControllerBase
{
    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(
        Guid patientId,
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default
    ) =>
        Ok(
            await mediator.Send(
                new GetPrescriptionsByPatientQuery(patientId, pageNumber, pageSize),
                ct
            )
        );

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetPrescriptionByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Create(
        [FromBody] CreatePrescriptionCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPost("{id:guid}/dispense")]
    [Authorize(Roles = "Pharmacist,Admin")]
    public async Task<IActionResult> Dispense(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new DispensePrescriptionCommand(id), ct));
}
