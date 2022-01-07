namespace CalifornianHealth.Data
{
    public interface IFetchConsultantCalendarOperation
    {
        Task<ConsultantAvailability<Date>?> FetchConsultantCalendar(int consultantId);
    }
}