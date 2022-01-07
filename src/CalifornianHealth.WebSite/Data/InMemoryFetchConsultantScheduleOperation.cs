namespace CalifornianHealth.Data
{
    public class InMemoryFetchConsultantScheduleOperation : IFetchConsultantScheduleOperation
    {
        public Task<ConsultantAvailability<Time>?> FetchConsultantSchedule(int consultantId,
            Date date)
        {
            var available = new List<Time>();

            for (var hour = 9; hour < 17; hour++)
            {
                available.Add(new Time(hour, minute: 0));
                available.Add(new Time(hour, minute: 30));
            }

            return Task.FromResult<ConsultantAvailability<Time>?>(
                new ConsultantAvailability<Time>(available));
        }
    }
}