namespace CalifornianHealth.Data
{
    public interface IFetchConsultantsOperation
    {
        Task<IReadOnlyList<Consultant>?> FetchConsultants();
    }
}