using System.ComponentModel.DataAnnotations.Schema;

namespace CalifornianHealth.Data
{
    [Table("ConsultantCalendar")]
    public class ConsultantCalendarEntity
    {
        public int Id { get; set; }
        public int ConsultantId { get; set; }
        public DateTime Date { get; set; }
        public bool Available { get; set; } = true;
    }
}