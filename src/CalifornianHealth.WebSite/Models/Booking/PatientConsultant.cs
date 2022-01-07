using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth.Models.Booking
{
    public class PatientConsultant : Patient
    {
        [Required]
        public int ConsultantId { get; set; }

        public PatientConsultant(PatientConsultant patient)
            : this((Patient)patient)
        {
            this.ConsultantId = patient.ConsultantId;
        }

        public PatientConsultant(Patient patient)
            : base(patient)
        { }

        public PatientConsultant() { }
    }
}