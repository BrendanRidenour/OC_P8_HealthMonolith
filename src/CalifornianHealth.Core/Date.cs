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

        public DateTime ToDateTime() =>
            new DateTime(year: this.Year, month: this.Month, day: this.Day,
                hour: 0, minute: 0, second: 0);

        public override string ToString() => $"{Year}-{Month.ToString("00")}-{Day.ToString("00")}";
        
        public static Date Parse(string value)
        {
            var sections = value.Split("-");

            return new Date(year: int.Parse(sections[0]),
                month: int.Parse(sections[1]),
                day: int.Parse(sections[2]));
        }
    }
}