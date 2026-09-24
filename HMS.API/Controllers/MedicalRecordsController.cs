using HMS.Application.Features.MedicalRecords.Commands;
using HMS.Application.Features.MedicalRecords.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/medical-records")]
[Authorize(Roles = "Admin,Doctor,Nurse")]
public class MedicalRecordsController(ISender mediator) : ControllerBase
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
                new GetMedicalRecordsByPatientQuery(patientId, pageNumber, pageSize),
                ct
            )
        );

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetMedicalRecordByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Create(
        [FromBody] CreateMedicalRecordCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPut("{id:guid}")]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] UpdateMedicalRecordCommand cmd,
        CancellationToken ct
    )
    {
        if (id != cmd.Id)
            return BadRequest("ID mismatch.");
        return Ok(await mediator.Send(cmd, ct));
    }
}
