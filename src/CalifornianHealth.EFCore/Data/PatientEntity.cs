using System.ComponentModel.DataAnnotations.Schema;

namespace CalifornianHealth.Data
{
    [Table("Patient")]
    public class PatientEntity : Patient
    {
        [Column("ID")]
        public override int Id { get; set; }
    }
}