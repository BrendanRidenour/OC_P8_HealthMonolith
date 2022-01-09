using CalifornianHealth.Concurrency;
using Microsoft.EntityFrameworkCore;

namespace CalifornianHealth.Data
{
    public class EntityFrameworkCreateAppointmentOperation : ICreateAppointmentOperation
    {
        private readonly CHDBContext _db;
        private readonly IConcurrencyService _concurrency;

        public EntityFrameworkCreateAppointmentOperation(CHDBContext db, IConcurrencyService concurrency)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
            this._concurrency = concurrency ?? throw new ArgumentNullException(nameof(concurrency));
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