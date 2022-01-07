﻿using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        [HttpPost("/appointment")]
        public Task<bool> FetchConsultantSchedule(
            [FromServices] Data.ICreateAppointmentOperation operation,
            [FromBody] Appointment appointment) => operation.CreateAppointment(appointment);
    }
}