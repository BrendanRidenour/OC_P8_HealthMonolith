using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth.Models.Booking
{
    public class PatientAppointment : PatientCalendarDate
    {
        [Required(ErrorMessage = "Please choose a time for the appointment.")]
        public string Time { get; set; } = null!;

        public PatientAppointment(PatientAppointment patient)
            : this((PatientCalendarDate)patient)
        {
            this.Time = patient.Time;
        }

        public PatientAppointment(PatientCalendarDate patient)
            : base(patient)
        { }

        public PatientAppointment() { }

        public Time ToTime() => CalifornianHealth.Time.Parse(this.Time);

        public DateTime ToStartDateTime()
        {
            var date = this.ToDate();
            var time = this.ToTime();

            return new DateTime(year: date.Year, month: date.Month, day: date.Day,
                hour: time.Hour, minute: time.Minute, second: 0);
        }
    }
}