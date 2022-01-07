using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth.Models.Booking
{
    public class PatientCalendarDate : PatientConsultant
    {
        [Required]
        public string Date { get; set; } = null!;

        public PatientCalendarDate(PatientCalendarDate patient)
            : this((PatientConsultant)patient)
        {
            this.Date = patient.Date;
        }

        public PatientCalendarDate(PatientConsultant patient)
            : base(patient)
        { }

        public PatientCalendarDate() { }

        public Date ToDate() => CalifornianHealth.Date.Parse(this.Date);
    }
}