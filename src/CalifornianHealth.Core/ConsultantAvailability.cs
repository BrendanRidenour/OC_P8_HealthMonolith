namespace CalifornianHealth
{
    public class ConsultantAvailability<T>
    {
        public IReadOnlyList<T> Available { get; set; }

        public ConsultantAvailability(IReadOnlyList<T> available)
        {
            this.Available = available;
        }
    }
}