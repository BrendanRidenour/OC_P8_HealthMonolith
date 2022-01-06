namespace CalifornianHealth.Data
{
    public class InMemoryFetchConsultantCalendarOperation : IFetchConsultantCalendarOperation
    {
        public Task<AvailableDates> FetchConsultantCalendar(int consultantId)
        {
            var availableDays = new List<Date>();

            for (var i = 0; i < 90; i++)
            {
                var date = DateTime.Now.AddDays(i);

                availableDays.Add(new Date(date));
            }

            var dates = new AvailableDates(availableDays);

            return Task.FromResult(dates);
        }
    }
}