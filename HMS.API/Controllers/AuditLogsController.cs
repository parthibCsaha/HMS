using HMS.Application.Features.AuditLogs.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/audit-logs")]
[Authorize(Roles = "Admin")]
public class AuditLogsController(ISender mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20,
        [FromQuery] Guid? userId = null, [FromQuery] string? action = null,
        [FromQuery] string? entityName = null, [FromQuery] DateTime? from = null,
        [FromQuery] DateTime? to = null, CancellationToken ct = default)
        => Ok(await mediator.Send(new GetAuditLogsQuery(pageNumber, pageSize, userId, action, entityName, from, to), ct));
}
