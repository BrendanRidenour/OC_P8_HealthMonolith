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
    }
}