using HMS.Application.Features.VitalSigns.Commands;
using HMS.Application.Features.VitalSigns.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/vital-signs")]
[Authorize(Roles = "Admin,Doctor,Nurse")]
public class VitalSignsController(ISender mediator) : ControllerBase
{
    [HttpGet("patient/{patientId:guid}")]
    public async Task<IActionResult> GetByPatient(Guid patientId, [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null, CancellationToken ct = default)
        => Ok(await mediator.Send(new GetVitalSignsQuery(patientId, from, to), ct));

    [HttpGet("patient/{patientId:guid}/latest")]
    public async Task<IActionResult> GetLatest(Guid patientId, CancellationToken ct)
        => Ok(await mediator.Send(new GetLatestVitalsQuery(patientId), ct));

    [HttpPost]
    public async Task<IActionResult> Record([FromBody] RecordVitalSignsCommand cmd, CancellationToken ct)
        => Ok(await mediator.Send(cmd, ct));
}
