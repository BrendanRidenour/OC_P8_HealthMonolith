namespace CalifornianHealth
{
    public class Appointment
    {
        public int Id { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public int ConsultantId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}