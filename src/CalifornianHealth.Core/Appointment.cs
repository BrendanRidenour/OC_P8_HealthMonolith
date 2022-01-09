using CalifornianHealth.Validation;
using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth
{
    public class Appointment
    {
        [Required]
        [DateTimeMinute(0, 30)]
        [DateTimeHour(MinValue = 9, MaxValue = 16)]
        [DateTimeAfter(validMinutesAfterNow: 30)]
        public virtual DateTime StartDateTime { get; set; }

        [Required]
        public virtual int ConsultantId { get; set; }

        [Required]
        public virtual Patient Patient { get; set; } = null!;
    }
}