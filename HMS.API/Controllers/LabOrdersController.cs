using HMS.Application.Features.LabOrders.Commands;
using HMS.Application.Features.LabOrders.Queries;
using HMS.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/lab-orders")]
[Authorize(Roles = "Admin,Doctor,LabTechnician")]
public class LabOrdersController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        [FromQuery] Guid? patientId = null,
        [FromQuery] LabTestStatus? status = null,
        CancellationToken ct = default
    ) =>
        Ok(await mediator.Send(new GetLabOrdersQuery(pageNumber, pageSize, patientId, status), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct) =>
        Ok(await mediator.Send(new GetLabOrderByIdQuery(id), ct));

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<IActionResult> Create(
        [FromBody] CreateLabOrderCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));

    [HttpPost("results")]
    [Authorize(Roles = "LabTechnician,Admin")]
    public async Task<IActionResult> AddResult(
        [FromBody] AddLabResultCommand cmd,
        CancellationToken ct
    ) => Ok(await mediator.Send(cmd, ct));
}
