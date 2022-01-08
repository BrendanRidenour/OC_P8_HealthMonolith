using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpPost("/appointment")]
        public async Task<bool> FetchConsultantSchedule([FromServices] Data.ICreateAppointmentOperation operation,
            [FromBody] Appointment appointment) => await operation.CreateAppointment(appointment);
    }
}