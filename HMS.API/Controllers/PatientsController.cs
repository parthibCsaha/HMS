using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using HMS.Application.Features.Patients.Queries.GetPatients;


namespace HMS.API.Controllers
{
    [Authorize]
    public class PatientsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetPatients(
            [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null, [FromQuery] string? sortBy = null,
            [FromQuery] bool descending = false, CancellationToken ct = default)
        {
            var result = await Mediator.Send(
                new GetPatientsQuery(pageNumber, pageSize, search, sortBy, descending), ct);
            return Ok(result);
        }
    }
}
