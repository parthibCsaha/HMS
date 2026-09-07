using HMS.Application.Features.Dashboard.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HMS.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(Roles = "Admin")]
public class DashboardController(ISender mediator) : ControllerBase
{
    [HttpGet("stats")]
    public async Task<IActionResult> GetStats(CancellationToken ct)
        => Ok(await mediator.Send(new GetDashboardStatsQuery(), ct));

    [HttpGet("occupancy")]
    public async Task<IActionResult> GetOccupancy(CancellationToken ct)
        => Ok(await mediator.Send(new GetOccupancyReportQuery(), ct));

    [HttpGet("appointment-trends")]
    public async Task<IActionResult> GetAppointmentTrends([FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken ct)
        => Ok(await mediator.Send(new GetAppointmentTrendsQuery(from, to), ct));

    [HttpGet("revenue")]
    public async Task<IActionResult> GetRevenue([FromQuery] DateTime from, [FromQuery] DateTime to, CancellationToken ct)
        => Ok(await mediator.Send(new GetRevenueReportQuery(from, to), ct));

    [HttpGet("patient-demographics")]
    public async Task<IActionResult> GetDemographics(CancellationToken ct)
        => Ok(await mediator.Send(new GetPatientDemographicsQuery(), ct));
}
