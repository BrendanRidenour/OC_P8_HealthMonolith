using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        /// <summary>
        /// A command operation to create and schedule an appointment
        /// </summary>
        /// <param name="appointment">The model to create and schedule an appointment</param>
        /// <response code="201">Returned if the appointment was successfully created and scheduled</response>
        /// <response code="409">Returned if the appointment could not be created due to a scheduling conflict</response>
        [HttpPost("/appointment")]
        public async Task<IActionResult> CreateAppointment(
            [FromServices] Data.ICreateAppointmentOperation operation,
            [FromBody] Appointment appointment)
        {
            var created = await operation.CreateAppointment(appointment);

            if (!created)
                return Conflict();

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}