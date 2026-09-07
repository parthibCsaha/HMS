using HMS.Application.Features.NursingNotes.Commands;
using HMS.Application.Features.NursingNotes.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/nursing-notes")]
[Authorize(Roles = "Admin,Nurse,Doctor")]
public class NursingNotesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
        [FromQuery] Guid? patientId = null, [FromQuery] Guid? admissionId = null, CancellationToken ct = default)
        => Ok(await mediator.Send(new GetNursingNotesQuery(pageNumber, pageSize, patientId, admissionId), ct));

    [HttpPost]
    [Authorize(Roles = "Nurse")]
    public async Task<IActionResult> Create([FromBody] CreateNursingNoteCommand cmd, CancellationToken ct)
        => Ok(await mediator.Send(cmd, ct));
}
