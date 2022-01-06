namespace CalifornianHealth
{
    public class Date
    {
        public int Year { get; }
        public int Month { get; }
        public int Day { get; }

        public Date(DateTime dateTime)
            : this(dateTime.Year, dateTime.Month, dateTime.Day)
        { }

        public Date(int year, int month, int day)
        {
            var date = new DateTime(year: year, month: month, day: day);

            this.Year = date.Year;
            this.Month = date.Month;
            this.Day = date.Day;
        }
    }
}