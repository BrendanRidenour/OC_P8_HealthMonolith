using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth
{
    public class ConsultantAvailability<T>
    {
        [Required]
        public IReadOnlyList<T> Available { get; set; } = null!;

        public ConsultantAvailability(IEnumerable<T> available)
            : this()
        {
            this.Available = available.ToArray();
        }

        public ConsultantAvailability() { }
    }
}