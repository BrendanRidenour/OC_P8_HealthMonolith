using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

namespace CalifornianHealth.Data
{
    public class EntityFrameworkFetchConsultantScheduleOperation : IFetchConsultantScheduleOperation
    {
        private readonly CHDBContext _db;
        private readonly ISystemClock _clock;

        public EntityFrameworkFetchConsultantScheduleOperation(CHDBContext db, ISystemClock clock)
        {
            this._db = db ?? throw new ArgumentNullException(nameof(db));
            this._clock = clock ?? throw new ArgumentNullException(nameof(clock));
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
    }
}