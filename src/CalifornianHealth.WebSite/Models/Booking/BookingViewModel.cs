namespace CalifornianHealth.Models.Booking
{
    public class BookingViewModel<TData, TPatient>
        where TPatient : Patient
    {
        public TData Data { get; }
        public TPatient Patient { get; }

        public BookingViewModel(TData data, TPatient patient)
        {
            this.Data = data;
            this.Patient = patient;
        }
    }
}