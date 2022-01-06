namespace CalifornianHealth.Data
{
    public interface ICreateAppointmentOperation
    {
        Task<bool> CreateAppointment(Appointment appointment);
    }
}