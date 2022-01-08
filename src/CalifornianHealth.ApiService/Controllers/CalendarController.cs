using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class CalendarController : ControllerBase
    {
        /// <summary>
        /// A query operation to retrieve the dates that a consultant is available to see patients
        /// </summary>
        /// <param name="consultantId">The id of the consultant to query</param>
        /// <returns>Returns the available dates that a consultant is available to see patients</returns>
        /// <response code="200">Returned if available dates for the consultant are found</response>
        /// <response code="404">Returned if no consultant is found by that id</response>
        [HttpGet("/consultants/{consultantId}")]
        public async Task<ActionResult<ConsultantAvailability<Date>?>> FetchConsultantCalendar(
            [FromServices] Data.IFetchConsultantCalendarOperation operation,
            [FromRoute] int consultantId)
        {
            var calendar = await operation.FetchConsultantCalendar(consultantId);

            if (calendar is null)
                return NotFound();

            return calendar;
        }
    }
}