namespace CalifornianHealth.Data
{
    public interface IFetchConsultantCalendarOperation
    {
        Task<AvailableDates?> FetchConsultantCalendar(int consultantId);
    }
}