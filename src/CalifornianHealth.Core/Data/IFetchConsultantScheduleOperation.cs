namespace CalifornianHealth.Data
{
    public interface IFetchConsultantScheduleOperation
    {
        Task<ConsultantAvailability<Time>?> FetchConsultantSchedule(int consultantId, Date date);
    }
}