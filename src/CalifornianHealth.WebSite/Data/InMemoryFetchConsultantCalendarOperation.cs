namespace CalifornianHealth.Data
{
    public class InMemoryFetchConsultantCalendarOperation : IFetchConsultantCalendarOperation
    {
        public Task<ConsultantAvailability<Date>?> FetchConsultantCalendar(int consultantId)
        {
            var availableDays = new List<Date>();

            for (var i = 0; i < 30; i++)
            {
                var date = DateTime.Now.AddDays(i);

                if (i % 2 == 0)
                    availableDays.Add(new Date(date));
            }

            var availableDates = new List<Date>(availableDays);

            return Task.FromResult<ConsultantAvailability<Date>?>(
                new ConsultantAvailability<Date>(availableDates));
        }
    }
}