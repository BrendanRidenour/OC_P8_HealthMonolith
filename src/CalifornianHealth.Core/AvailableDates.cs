using System.Collections;

namespace CalifornianHealth
{
    public class AvailableDates : IReadOnlyList<Date>
    {
        private readonly List<Date> _dates = new List<Date>();

        public AvailableDates(IEnumerable<Date> dates)
        {
            this._dates.AddRange(dates);
        }

        public Date this[int index] => this._dates[index];

        public int Count => this._dates.Count;

        public IEnumerator<Date> GetEnumerator() => this._dates.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this._dates.GetEnumerator();
    }
}