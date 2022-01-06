namespace CalifornianHealth.Models.Booking
{
    public class PatientAppointment : Patient
    {
        public int Year { get; set; }
        public int Month { get; set; }
        public int Day { get; set; }
        public int StartHour { get; set; }
        public int StartMinute { get; set; }

        public (DateTime startDateTime, DateTime endDateTime) ToDates()
        {
            var date = new Date(year: Year, month: Month, day: Day);

            var startDateTime = new DateTime(year: Year, month: Month, day: Day,
                hour: StartHour, minute: StartMinute, second: 0);

            var endDateTime = startDateTime.AddMinutes(30);

            return (startDateTime, endDateTime);
        }
    }
}