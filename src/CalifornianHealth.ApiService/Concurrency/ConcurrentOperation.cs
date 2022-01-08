namespace CalifornianHealth.Concurrency
{
    public class ConcurrentOperation : IDisposable
    {
        private readonly TaskCompletionSource _operationSource;

        public ConcurrentOperation(TaskCompletionSource operationSource)
        {
            this._operationSource = operationSource ?? throw new ArgumentNullException(nameof(operationSource));
        }

        public void Dispose()
        {
            this._operationSource.TrySetResult();
        }
    }
}