using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class ConsultantsController : ControllerBase
    {
        /// <summary>
        /// A query operation to retrieve all consultants employed by the hospital
        /// </summary>
        /// <returns>Returns a list of active consultants</returns>
        /// <response code="200">Returned if a list of active consultants is found</response>
        /// <response code="404">Returned if no active consultants are found</response>
        [HttpGet("/consultants")]
        public async Task<ActionResult<IReadOnlyList<Consultant>?>> FetchConsultants(
            [FromServices] Data.IFetchConsultantsOperation operation)
        {
            var consultants = await operation.FetchConsultants();

            if (consultants is null)
                return NotFound();

            return consultants.ToArray();
        }
    }
}