using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MediatR;


namespace HMS.API.Controllers
{
    [Authorize]
    public class PatientsController : ControllerBase
    {
        [HttpGet]
        [Route("api/patients/hello")]
        public async Task<IActionResult> Hello()
        {
            return Ok("Hello from PatientsController!");
        }
        //public async Task<IActionResult> GetPatients(
        //    [FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10,
        //    [FromQuery] string? search = null, [FromQuery] string? sortBy = null,
        //    [FromQuery] bool descending = false, CancellationToken ct = default)
        //{
        //    var result = await Mediator.Send(
        //        new GetPatientsQuery(pageNumber, pageSize, search, sortBy, descending), ct);
        //    return Ok(result);
        //}
    }
}
