namespace CalifornianHealth.Models.Booking
{
    public class BookingViewModel<T>
    {
        public T Data { get; }
        public Patient? Patient { get; }

        public BookingViewModel(T model, Patient? patient)
        {
            this.Data = model;
            this.Patient = patient;
        }
    }
}