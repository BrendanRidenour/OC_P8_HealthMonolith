namespace CalifornianHealth
{
    public class Time
    {
        public int Hour { get; }
        public int Minute { get; }

        public Time(int hour, int minute)
        {
            if (hour < 0 || hour > 23)
                throw new ArgumentOutOfRangeException(nameof(hour), message: "Argument must be between 0 and 23");

            if (minute < 0 || minute > 59)
                throw new ArgumentOutOfRangeException(nameof(minute), message: "Argument must be between 0 ad 59");

            this.Hour = hour;
            this.Minute = minute;
        }

        public override string ToString() => new DateTime(year: 2000, month: 1, day: 1,
            hour: this.Hour, minute: this.Minute, second: 0).ToString("t");

        public static Time Parse(string value)
        {
            var dt = DateTime.Parse($"1/1/2000 {value}");

            return new Time(hour: dt.Hour, dt.Minute);
        }
    }
}