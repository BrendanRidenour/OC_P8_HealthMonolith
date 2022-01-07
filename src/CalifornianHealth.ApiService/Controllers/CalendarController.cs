using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class CalendarController : ControllerBase
    {
        [HttpGet("/consultants/{consultantId}")]
        public async Task<ActionResult<ConsultantAvailability<Date>?>> FetchConsultantCalendar(
            [FromServices] Data.IFetchConsultantCalendarOperation operation,
            [FromRoute]int consultantId)
        {
            var calendar = await operation.FetchConsultantCalendar(consultantId);

            if (calendar is null)
                return NotFound();

            return calendar;
        }
}
}