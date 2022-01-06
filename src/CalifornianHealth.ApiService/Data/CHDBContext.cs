using Microsoft.EntityFrameworkCore;

namespace CalifornianHealth.Data
{
    public class CHDBContext : DbContext
    {
        public DbSet<Appointment> Appointments { get; set; } = null!;
        public DbSet<Consultant> Consultants { get; set; } = null!;
        public DbSet<ConsultantCalendar> ConsultantCalendars { get; set; } = null!;
        public DbSet<Patient> Patients { get; set; } = null!;
    }
}