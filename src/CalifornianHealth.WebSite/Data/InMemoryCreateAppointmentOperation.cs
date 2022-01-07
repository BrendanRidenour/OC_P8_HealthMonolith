namespace CalifornianHealth.Data
{
    public class InMemoryCreateAppointmentOperation : ICreateAppointmentOperation
    {
        public Task<bool> CreateAppointment(Appointment appointment)
        {
            var result = true;

            if (appointment.StartDateTime.Minute == 30)
                result = false;

            return Task.FromResult(result);
        }
    }
}