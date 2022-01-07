using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth
{
    public class Appointment
    {
        [Required]
        public virtual DateTime StartDateTime { get; set; }

        [Required]
        public virtual int ConsultantId { get; set; }

        [Required]
        public virtual Patient Patient { get; set; } = null!;
    }
}