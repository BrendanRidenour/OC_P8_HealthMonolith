using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace CalifornianHealth.Concurrency
{
    public class ConcurrentQueueConcurrencyServiceTests
    {
        [Theory]
        [InlineData(100)]
        public async Task RunTestCase(int concurrentUsers)
        {
            var tasks = new List<Task<DateTime>>();

            for (var i = 0; i < concurrentUsers; i++)
            {
                tasks.Add(Run());
            }

            await Task.WhenAll(tasks);

            var dates = tasks.Select(t => t.Result).ToList();

            Assert.Equal(dates.Count, dates.Distinct().Count());
        }

        static async Task<DateTime> Run()
        {
            var service = new ConcurrentQueueConcurrencyService();

            using var operation = await service.EnterQueue();

            var now = DateTime.Now;

            await Task.Delay(1);

            return now;
        }
    }
}