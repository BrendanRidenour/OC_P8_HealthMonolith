using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class ScheduleController : ControllerBase
    {
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