using System.Collections.Concurrent;

namespace CalifornianHealth.Concurrency
{
    public class ConcurrentQueueConcurrencyService : IConcurrencyService
    {
        private static readonly ConcurrentQueue<TaskCompletionSource<ConcurrentOperation>> queue = new ConcurrentQueue<TaskCompletionSource<ConcurrentOperation>>();

        public Task<ConcurrentOperation> EnterQueue()
        {
            var queueSource = new TaskCompletionSource<ConcurrentOperation>();

            queue.Enqueue(queueSource);

            ConfirmQueueExecuting();

            return queueSource.Task;
        }

        private static bool queueExecuting = false;
        private static readonly object queueExecuting_Lock = new object();
        private static async void ConfirmQueueExecuting()
        {
            lock (queueExecuting_Lock)
            {
                if (queueExecuting)
                    return;

                queueExecuting = true;
            }

            try
            {
                while (!queue.IsEmpty)
                {
                    if (queue.TryDequeue(out var queueSource))
                    {
                        var operationSource = new TaskCompletionSource();
                        var operation = new ConcurrentOperation(operationSource);

                        queueSource.SetResult(operation);

                        await operationSource.Task;
                    }
                }
            }
            finally
            {
                lock (queueExecuting_Lock)
                {
                    queueExecuting = false;
                }
            }
        }
    }
}