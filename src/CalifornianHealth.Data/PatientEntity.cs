using System.ComponentModel.DataAnnotations.Schema;

namespace CalifornianHealth.Data
{
    [Table("Patient")]
    public class PatientEntity : Patient
    {
        [Column("ID")]
        public int Id { get; set; }

        public PatientEntity(Patient patient)
            : base(patient)
        { }

        public PatientEntity() { }
    }
}