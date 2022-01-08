using CalifornianHealth.Concurrency;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace CalifornianHealth.Data
{
    public class EntityFrameworkOperationService
        : IFetchConsultantsOperation, IFetchConsultantCalendarOperation, IFetchConsultantScheduleOperation,
        ICreateAppointmentOperation
    {
        private readonly CHDBContext _db;
        private readonly IConcurrencyService _concurrency;
        private readonly ISystemClock _clock;

        public EntityFrameworkOperationService(CHDBContext db, IConcurrencyService concurrency,
            ISystemClock clock)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
            this._concurrency = concurrency ?? throw new ArgumentNullException(nameof(concurrency));
            this._clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public async Task<IReadOnlyList<Consultant>?> FetchConsultants() =>
            await this._db.Consultants.ToListAsync();

        public async Task<ConsultantAvailability<Date>?> FetchConsultantCalendar(int consultantId)
        {
            var calendar = await this._db.ConsultantCalendars
                .Where(e => e.ConsultantId == consultantId && e.Available && e.Date >= _clock.UtcNow.Date)
                .ToListAsync();

            var dates = calendar.Select(e => new Date(e.Date));

            return new ConsultantAvailability<Date>(dates);
        }

        public async Task<ConsultantAvailability<Time>?> FetchConsultantSchedule(int consultantId, Date date)
        {
            var schedule = await this._db.Appointments
                .Where(e => e.ConsultantId == consultantId && e.StartDateTime.Date == date.ToDateTime().Date)
                .ToListAsync();

            var times = new List<Time>();
            for (var hour = 9; hour < 17; hour++)
            {
                var start = new DateTime(year: date.Year, month: date.Month, day: date.Day,
                    hour: hour, minute: 0, second: 0);
                
                // Consider if LocalDateTime is best for this use case. Works for now.
                var now = _clock.UtcNow.LocalDateTime;

                if (start >= now.AddMinutes(30) &&
                    !schedule.Where(e => e.StartDateTime == start).Any())
                {
                    times.Add(new Time(hour, 00));
                }

                start = start.AddMinutes(30);

                if (start >= now.AddMinutes(30) &&
                    !schedule.Where(e => e.StartDateTime == start).Any())
                {
                    times.Add(new Time(hour, 30));
                }
            }

            return new ConsultantAvailability<Time>(times);
        }

        public async Task<bool> CreateAppointment(Appointment appointment)
        {
            using var queue = await this._concurrency.EnterQueue();

            if (await AppointmentNotPossible(appointment.ConsultantId, appointment.StartDateTime))
                return false;

            var patientEntity = new PatientEntity(appointment.Patient);

            var appointmentEntity = new AppointmentEntity()
            {
                StartDateTime = appointment.StartDateTime,
                EndDateTime = appointment.StartDateTime.AddMinutes(30),
                ConsultantId = appointment.ConsultantId,
                Patient = patientEntity,
            };

            this._db.Appointments.Add(appointmentEntity);

            await this._db.SaveChangesAsync();

            return true;
        }

        async Task<bool> AppointmentNotPossible(int consultantId, DateTime startDateTime)
        {
            var consultantAvailableOnDate = await this._db.ConsultantCalendars
                .Where(e => e.ConsultantId == consultantId && e.Date == startDateTime.Date && e.Available)
                .SingleOrDefaultAsync();

            if (consultantAvailableOnDate is null)
                return false;

            var existingAppointment = await this._db.Appointments
               .Where(e => e.ConsultantId == consultantId && e.StartDateTime == startDateTime)
               .SingleOrDefaultAsync();

            return existingAppointment is not null;
        }
    }
}