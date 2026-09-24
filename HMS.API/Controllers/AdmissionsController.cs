using System.Security.Claims;
using HMS.Application.Features.Admissions.Commands;
using HMS.Application.Features.Admissions.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Doctor,Nurse")]
public class AdmissionsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? patientId = null,
        [FromQuery] bool? isActive = null,
        CancellationToken ct = default
    )
    {
        var result = await mediator.Send(
            new GetAdmissionsQuery(pageNumber, pageSize, patientId, isActive),
            ct
        );
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetAdmissionByIdQuery(id), ct));

    [HttpPost("admit")]
    public async Task<IActionResult> Admit(
        [FromBody] AdmitPatientCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPost("{id:guid}/discharge")]
    public async Task<IActionResult> Discharge(
        Guid id,
        [FromBody] DischargePatientCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd with { Id = id }, ct));

    [HttpPost("{id:guid}/transfer")]
    public async Task<IActionResult> Transfer(
        Guid id,
        [FromBody] TransferPatientCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd with { Id = id }, ct));
}
