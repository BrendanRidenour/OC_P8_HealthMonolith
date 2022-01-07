using System.ComponentModel.DataAnnotations.Schema;

namespace CalifornianHealth.Data
{
    [Table("Appointment")]
    public class AppointmentEntity
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ConsultantId { get; set; }
        public int PatientId { get; set; }

        public virtual ConsultantEntity Consultant { get; set; } = null!;
        public virtual PatientEntity Patient { get; set; } = null!;
    }
}