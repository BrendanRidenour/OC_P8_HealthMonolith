namespace CalifornianHealth
{
    public class Appointment
    {
        public DateTime StartDateTime { get; set; }
        public int ConsultantId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}