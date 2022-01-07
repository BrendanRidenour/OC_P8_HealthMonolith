namespace CalifornianHealth
{
    public class Appointment
    {
        public virtual int Id { get; set; }
        public virtual DateTime StartDateTime { get; set; }
        public virtual int ConsultantId { get; set; }
        public virtual Patient Patient { get; set; } = null!;
    }
}