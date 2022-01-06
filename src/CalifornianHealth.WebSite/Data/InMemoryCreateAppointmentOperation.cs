namespace CalifornianHealth.Data
{
    public class InMemoryCreateAppointmentOperation : ICreateAppointmentOperation
    {
        public Task<bool> CreateAppointment(Appointment appointment) => Task.FromResult(true);
    }
}