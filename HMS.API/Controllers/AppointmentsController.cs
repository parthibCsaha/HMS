using HMS.Application.Features.Appointments.Commands;
using HMS.Application.Features.Appointments.Queries;
using HMS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AppointmentsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] string? searchTerm = null,
        [FromQuery] Guid? doctorId = null,
        [FromQuery] Guid? patientId = null,
        [FromQuery] AppointmentStatus? status = null,
        [FromQuery] DateTime? date = null,
        CancellationToken ct = default
    ) =>
        Ok(
            await mediator.Send(
                new GetAppointmentsQuery(
                    pageNumber,
                    pageSize,
                    searchTerm,
                    doctorId,
                    patientId,
                    status,
                    date
                ),
                ct
            )
        );

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetAppointmentByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Admin,Receptionist,Doctor")]
    public async Task<IActionResult> Create(
        [FromBody] CreateAppointmentCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPost("{id:guid}/cancel")]
    [Authorize(Roles = "Admin,Receptionist,Doctor")]
    public async Task<IActionResult> Cancel(
        Guid id,
        [FromBody] CancelAppointmentCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd with { Id = id }, ct));

    [HttpPost("{id:guid}/check-in")]
    [Authorize(Roles = "Admin,Receptionist,Nurse")]
    public async Task<IActionResult> CheckIn(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new CheckInAppointmentCommand(id), ct));

    [HttpPost("{id:guid}/complete")]
    [Authorize(Roles = "Admin,Doctor")]
    public async Task<IActionResult> Complete(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new CompleteAppointmentCommand(id), ct));
}
