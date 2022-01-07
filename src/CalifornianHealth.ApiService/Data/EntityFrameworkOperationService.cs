using Microsoft.EntityFrameworkCore;

namespace CalifornianHealth.Data
{
    public class EntityFrameworkOperationService
        : IFetchConsultantsOperation, IFetchConsultantCalendarOperation, IFetchConsultantScheduleOperation,
        ICreateAppointmentOperation
    {
        private readonly CHDBContext _db;

        public EntityFrameworkOperationService(CHDBContext db)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
        }

        public async Task<IReadOnlyList<Consultant>?> FetchConsultants() =>
            await this._db.Consultants.ToListAsync();

        public Task<ConsultantAvailability<Date>?> FetchConsultantCalendar(int consultantId)
        {
            return Task.FromResult<ConsultantAvailability<Date>?>(null);
        }

        public Task<ConsultantAvailability<Time>?> FetchConsultantSchedule(int consultantId, Date date)
        {
            return Task.FromResult<ConsultantAvailability<Time>?>(null);
        }

        public Task<bool> CreateAppointment(Appointment appointment)
        {
            return Task.FromResult(false);
        }
    }
}