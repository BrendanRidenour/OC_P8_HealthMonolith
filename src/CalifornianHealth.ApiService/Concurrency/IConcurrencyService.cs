namespace CalifornianHealth.Concurrency
{
    public interface IConcurrencyService
    {
        Task<ConcurrentOperation> EnterQueue();
    }
}