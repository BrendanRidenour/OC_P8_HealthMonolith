using CalifornianHealth.Data;
using CalifornianHealth.Models.Booking;
using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [Route("/book-appointment")]
    public class BookingController : Controller
    {
        [HttpGet]
        public Task<IActionResult> SelectConsultant([FromServices] IFetchConsultantsOperation operation) =>
            this.SelectConsultantView(operation);

        [HttpPost]
        public Task<IActionResult> SelectConsultant([FromServices] IFetchConsultantsOperation operation,
            [FromForm]Patient patient) =>
            this.SelectConsultantView(operation, patient);

        protected async Task<IActionResult> SelectConsultantView(IFetchConsultantsOperation operation,
            Patient? patient = null)
        {
            var consultants = await operation.FetchConsultants();

            return View(viewName: "SelectConsultant",
                model: new BookingViewModel<IReadOnlyList<Consultant>>(consultants, patient));
        }

        [HttpGet]
        [Route("{consultantId}")]
        public Task<IActionResult> ConsultantCalendar([FromServices] IFetchConsultantCalendarOperation operation,
            [FromRoute] int consultantId) =>
            this.ConsultantCalendarView(operation, consultantId);

        [HttpPost]
        [Route("{consultantId}")]
        public Task<IActionResult> ConsultantCalendar([FromServices] IFetchConsultantCalendarOperation operation,
            [FromRoute] int consultantId, [FromForm]Patient patient) =>
            this.ConsultantCalendarView(operation, consultantId, patient);

        protected async Task<IActionResult> ConsultantCalendarView(IFetchConsultantCalendarOperation operation,
            int consultantId, Patient? patient = null)
        {
            var calendar = await operation.FetchConsultantCalendar(consultantId);

            if (calendar is null)
                return View("NotFound");

            return View(viewName: "ConsultantCalendar",
                model: new BookingViewModel<AvailableDates>(calendar, patient));
        }

        [HttpGet]
        [Route("{consultantId}/patient")]
        public IActionResult PatientAppointment() =>
            this.PatientAppointmentView(new PatientAppointment());

        [HttpPost]
        [Route("{consultantId}/patient")]
        public async Task<IActionResult> PatientAppointment([FromServices] ICreateAppointmentOperation operation,
            [FromRoute] int consultantId, [FromForm] PatientAppointment patientAppointment)
        {
            if (!ModelState.IsValid)
                return this.PatientAppointmentView(patientAppointment);

            var (startDateTime, endDateTime) = patientAppointment.ToDates();

            return await this.CreateAppointmentView(operation, new Appointment()
            {
                ConsultantId = consultantId,
                StartDateTime = startDateTime,
                EndDateTime = endDateTime,
                Patient = patientAppointment,
            });
        }

        protected IActionResult PatientAppointmentView(PatientAppointment patientAppointment) =>
            View(viewName: "PatientAppointment", model: patientAppointment);

        protected async Task<IActionResult> CreateAppointmentView(ICreateAppointmentOperation operation,
            Appointment appointment)
        {
            var success = await operation.CreateAppointment(appointment);

            var viewName = success ? "CreateAppointmentSucceeded" : "CreateAppointmentFailed";

            return View(viewName, model: appointment);
        }
    }
}