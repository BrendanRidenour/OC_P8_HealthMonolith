namespace CalifornianHealth.Data
{
    public interface IFetchConsultantDatesOperation
    {
        Task<ConsultantAvailability<Date>?> FetchConsultantDates(int consultantId);
    }
}