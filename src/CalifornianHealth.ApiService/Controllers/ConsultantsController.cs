using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class ConsultantsController : ControllerBase
    {
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