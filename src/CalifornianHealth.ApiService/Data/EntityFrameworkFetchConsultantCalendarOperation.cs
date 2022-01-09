using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace CalifornianHealth.Data
{
    public class EntityFrameworkFetchConsultantCalendarOperation : IFetchConsultantCalendarOperation
    {
        private readonly CHDBContext _db;
        private readonly ISystemClock _clock;

        public EntityFrameworkFetchConsultantCalendarOperation(CHDBContext db, ISystemClock clock)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
            this._clock = clock ?? throw new ArgumentNullException(nameof(clock));
        }

        public async Task<ConsultantAvailability<Date>?> FetchConsultantCalendar(int consultantId)
        {
            var calendar = await this._db.ConsultantCalendars
                .Where(e => e.ConsultantId == consultantId && e.Available && e.Date >= _clock.UtcNow.Date)
                .ToListAsync();

            var dates = calendar.Select(e => new Date(e.Date));

            return new ConsultantAvailability<Date>(dates);
        }
    }
}