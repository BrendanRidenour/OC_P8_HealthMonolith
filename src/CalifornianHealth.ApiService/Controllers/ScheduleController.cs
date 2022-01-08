using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        /// <summary>
        /// A query operation to retrieve the avaiable times on a particular day that a consultant is available to see patients
        /// </summary>
        /// <param name="consultantId">The id of the consultant to query</param>
        /// <param name="date">The date to query for available times</param>
        /// <returns>Returns the available times on a particular day that a consultant is available to see patients</returns>
        /// <response code="200">Returned if available times are found for the consultant on a particular day</response>
        /// <response code="400">Returned if the date supplied is misformed or invalid</response>
        /// <response code="404">Returned if no consultant is found by that id</response>
        [HttpGet("/consultants/{consultantId}/schedule/{date}")]
        public async Task<ActionResult<ConsultantAvailability<Time>?>> FetchConsultantSchedule(
            [FromServices] Data.IFetchConsultantScheduleOperation operation,
            [FromRoute] int consultantId, [FromRoute] string date)
        {
            if (!Date.TryParse(date, out var scheduleDate))
                return BadRequest();

            var schedule = await operation.FetchConsultantSchedule(consultantId, scheduleDate!);

            if (schedule is null)
                return NotFound();

            return schedule;
        }
    }
}