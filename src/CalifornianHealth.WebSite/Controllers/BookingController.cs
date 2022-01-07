using CalifornianHealth.Data;
using CalifornianHealth.Models.Booking;
using Microsoft.AspNetCore.Mvc;

namespace CalifornianHealth.Controllers
{
    [Route("/book-appointment")]
    public class BookingController : Controller
    {
        private readonly IFetchConsultantsOperation _consultantsOperation;
        private readonly IFetchConsultantDatesOperation _datesOperation;
        private readonly IFetchConsultantScheduleOperation _scheduleOperation;
        private readonly ICreateAppointmentOperation _appointmentOperation;

        public BookingController(IFetchConsultantsOperation consultantsOperation,
            IFetchConsultantDatesOperation datesOperation,
            IFetchConsultantScheduleOperation scheduleOperation,
            ICreateAppointmentOperation appointmentOperation)
        {
            this._consultantsOperation = consultantsOperation ?? throw new ArgumentNullException(nameof(consultantsOperation));
            this._datesOperation = datesOperation ?? throw new ArgumentNullException(nameof(datesOperation));
            this._scheduleOperation = scheduleOperation ?? throw new ArgumentNullException(nameof(scheduleOperation));
            this._appointmentOperation = appointmentOperation ?? throw new ArgumentNullException(nameof(appointmentOperation));
        }

        [HttpGet]
        public Task<IActionResult> PatientInformation() =>
            PatientInformationView(new Patient());

        [HttpPost]
        public Task<IActionResult> PatientInformation([FromForm] Patient patient) =>
            PatientInformationView(patient);

        [HttpPost]
        [Route("consultant")]
        public Task<IActionResult> SelectConsultant([FromForm] Patient patient)
        {
            if (!ModelState.IsValid)
                return PatientInformationView(patient);

            return SelectConsultantView(new PatientConsultant(patient));
        }

        [HttpPost]
        [Route("consultant/calendar")]
        public Task<IActionResult> ConsultantCalendar([FromForm] PatientConsultant patient)
        {
            if (!ModelState.IsValid)
            {
                if (patient.ConsultantId == 0)
                    return SelectConsultantView(patient);
                else
                    return PatientInformationView(patient);
            }

            return ConsultantCalendarView(new PatientCalendarDate(patient));
        }

        [HttpPost]
        [Route("consultant/calendar/schedule")]
        public Task<IActionResult> ScheduleTime([FromForm] PatientCalendarDate patient)
        {
            if (!ModelState.IsValid)
                return ConsultantCalendarView(patient);

            return ScheduleTimeView(new PatientAppointment(patient));
        }

        [HttpPost]
        [Route("consultant/calendar/schedule/appointment")]
        public Task<IActionResult> CreateAppointment([FromForm] PatientAppointment patient)
        {
            if (!ModelState.IsValid)
                return ScheduleTimeView(patient);

            return CreateAppointmentView(patient);
        }

        protected Task<IActionResult> PatientInformationView(Patient patient) =>
            Task.FromResult<IActionResult>(
                View(viewName: nameof(PatientInformation),
                    model: patient ?? throw new ArgumentNullException(nameof(patient))));

        protected async Task<IActionResult> SelectConsultantView(PatientConsultant patient)
        {
            var consultants = await this._consultantsOperation.FetchConsultants();

            var viewModel = new BookingViewModel<IReadOnlyList<Consultant>, PatientConsultant>(
                consultants, patient);

            return View(viewName: nameof(SelectConsultant), model: viewModel);
        }

        protected async Task<IActionResult> ConsultantCalendarView(PatientCalendarDate patient)
        {
            var calendar = await this._datesOperation.FetchConsultantDates(
                patient.ConsultantId);

            if (calendar is null)
                return NotFound();

            var viewModel = new BookingViewModel<ConsultantAvailability<Date>, PatientCalendarDate>(
                calendar, patient);

            return View(viewName: nameof(ConsultantCalendar), model: viewModel);
        }

        protected async Task<IActionResult> ScheduleTimeView(PatientAppointment patient)
        {
            var schedule = await _scheduleOperation.FetchConsultantSchedule(patient.ConsultantId,
                patient.ToDate());

            if (schedule is null)
                return NotFound();

            var viewModel = new BookingViewModel<ConsultantAvailability<Time>, PatientAppointment>(
                schedule, patient);

            return View(viewName: nameof(ScheduleTime), model: viewModel);
        }

        protected async Task<IActionResult> CreateAppointmentView(PatientAppointment patient)
        {
            var appointment = new Appointment()
            {
                ConsultantId = patient.ConsultantId,
                StartDateTime = patient.ToStartDateTime(),
                Patient = patient,
            };

            var success = await this._appointmentOperation.CreateAppointment(appointment);

            var viewModel = new BookingViewModel<Appointment, PatientAppointment>(
                appointment, patient);

            var viewName = success ? "CreateAppointmentSucceeded" : "CreateAppointmentFailed";

            return View(viewName, model: viewModel);
        }
    }
}