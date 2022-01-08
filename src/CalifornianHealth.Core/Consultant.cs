using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth
{
    public class Consultant
    {
        [Required]
        public virtual int Id { get; set; }

        [Required]
        [StringLength(100)]
        public virtual string FName { get; set; } = null!;

        [Required]
        [StringLength(100)]
        public virtual string LName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public virtual string Speciality { get; set; } = null!;

        public override string ToString() => $"{FName} {LName}, {Speciality}".Trim();
    }
}