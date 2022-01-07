using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth
{
    public class Consultant
    {
        public virtual int Id { get; set; }

        [StringLength(100)]
        public virtual string FName { get; set; } = null!;

        [StringLength(100)]
        public virtual string LName { get; set; } = null!;

        [StringLength(50)]
        public virtual string Speciality { get; set; } = null!;

        public override string ToString() => $"{FName} {LName}, {Speciality}".Trim();
    }
}