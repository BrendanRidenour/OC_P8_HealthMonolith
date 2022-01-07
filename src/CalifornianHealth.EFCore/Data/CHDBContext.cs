using Microsoft.EntityFrameworkCore;

namespace CalifornianHealth.Data
{
    public class CHDBContext : DbContext
    {
        public DbSet<AppointmentEntity> Appointments { get; set; } = null!;
        public DbSet<ConsultantEntity> Consultants { get; set; } = null!;
        public DbSet<ConsultantCalendarEntity> ConsultantCalendars { get; set; } = null!;
        public DbSet<PatientEntity> Patients { get; set; } = null!;
    }
}