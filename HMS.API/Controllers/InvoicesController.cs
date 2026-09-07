using HMS.Application.Features.Invoices.Commands;
using HMS.Application.Features.Invoices.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin,Receptionist")]
public class InvoicesController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
        [FromQuery] Guid? patientId = null, CancellationToken ct = default)
        => Ok(await mediator.Send(new GetInvoicesQuery(pageNumber, pageSize, patientId), ct));

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new GetInvoiceByIdQuery(id), ct));

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateInvoiceCommand cmd, CancellationToken ct)
        => Ok(await mediator.Send(cmd, ct));

    [HttpPost("{id:guid}/payment")]
    public async Task<IActionResult> AddPayment(Guid id, [FromBody] AddPaymentCommand cmd, CancellationToken ct)
        => Ok(await mediator.Send(cmd with { InvoiceId = id }, ct));

    [HttpPost("{id:guid}/cancel")]
    public async Task<IActionResult> Cancel(Guid id, CancellationToken ct)
        => Ok(await mediator.Send(new CancelInvoiceCommand(id), ct));
}
