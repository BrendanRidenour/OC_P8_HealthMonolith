using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth
{
#warning Model validation: Only hour 9-16, 30 minute intervals, must be (how far?) in the future
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