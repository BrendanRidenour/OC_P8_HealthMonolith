using CalifornianHealth.Concurrency;
using CalifornianHealth.Controllers;
using CalifornianHealth.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Xunit;

namespace CalifornianHealth
{
    public class TestConcurrentUsers
    {
        [Theory]
        [InlineData(3000)]
        public async Task RunTestCase(int concurrentUsers)
        {
            // Arrange
            SeedDbContext();
            var tasks = new List<Task<bool>>();

            // Act
            for (var i = 0; i < concurrentUsers; i++)
            {
                tasks.Add(OneUserCallsCreateAppointment());
            }

            await Task.WhenAll(tasks);

            var results = tasks.Select(t => t.Result).ToList();

            // Assert
            Assert.Equal(concurrentUsers, results.Count);

            var db = DbContext();

            var consultant1Appointments = await db.Appointments
                .Where(a => a.ConsultantId == 1).ToListAsync();

            var consultant1DistinctAppointments = consultant1Appointments
                .DistinctBy(e => e.StartDateTime).ToList();

            Assert.Equal(consultant1Appointments.Count, consultant1DistinctAppointments.Count);

            var consultant2Appointments = await db.Appointments
                .Where(a => a.ConsultantId == 2).ToListAsync();

            var consultant2DistinctAppointments = consultant2Appointments
                .DistinctBy(e => e.StartDateTime).ToList();

            Assert.Equal(consultant2Appointments.Count, consultant2DistinctAppointments.Count);

            var consultant3Appointments = await db.Appointments
                .Where(a => a.ConsultantId == 3).ToListAsync();

            var consultant3DistinctAppointments = consultant3Appointments
                .DistinctBy(e => e.StartDateTime).ToList();

            Assert.Equal(consultant3Appointments.Count, consultant3DistinctAppointments.Count);

            var consultant4Appointments = await db.Appointments
                .Where(a => a.ConsultantId == 4).ToListAsync();

            var consultant4DistinctAppointments = consultant4Appointments
                .DistinctBy(e => e.StartDateTime).ToList();

            Assert.Equal(consultant4Appointments.Count, consultant4DistinctAppointments.Count);
        }

        async Task<bool> OneUserCallsCreateAppointment()
        {
            var controller = new AppointmentController();
            var db = DbContext();
            var operation = Operation(db);
            var appointment = Appointment();

            var result = await controller.CreateAppointment(operation, appointment);

            if (result is StatusCodeResult status && status.StatusCode == 201)
                return true;

            return false;
        }

        readonly DbContextOptions<CHDBContext> dbContextOptions = new DbContextOptionsBuilder<CHDBContext>()
            .UseInMemoryDatabase(databaseName: "CH")
            .Options;
        void SeedDbContext()
        {
            using var context = DbContext();

            context.Consultants.AddRange(new List<ConsultantEntity>()
            {
                new ConsultantEntity()
                {
                    Id = 1,
                    FName = "Firstname",
                    LName = "Lastname",
                    Speciality = "Doctor",
                },
                new ConsultantEntity()
                {
                    Id = 2,
                    FName = "Firstname",
                    LName = "Lastname",
                    Speciality = "Doctor",
                },
                new ConsultantEntity()
                {
                    Id = 3,
                    FName = "Firstname",
                    LName = "Lastname",
                    Speciality = "Doctor",
                },
                new ConsultantEntity()
                {
                    Id = 4,
                    FName = "Firstname",
                    LName = "Lastname",
                    Speciality = "Doctor",
                },
            });

            var calendars = new List<ConsultantCalendarEntity>();

            for (var consultantId = 1; consultantId <= 4; consultantId++)
                for (var day = 1; day <= 31; day++)
                {
                    calendars.Add(new ConsultantCalendarEntity()
                    {
                        Id = int.Parse($"{consultantId}{day}"),
                        ConsultantId = consultantId,
                        Date = new DateTime(year: 2022, month: 1, day: day),
                        Available = true,
                    });
                }

            context.ConsultantCalendars.AddRange(calendars);

            context.SaveChanges();
        }
        CHDBContext DbContext() => new CHDBContext(dbContextOptions);
        static EntityFrameworkCreateAppointmentOperation Operation(CHDBContext db) =>
            new EntityFrameworkCreateAppointmentOperation(
                db: db,
                concurrency: new ConcurrentQueueConcurrencyService());
        static Appointment Appointment() => new Appointment()
        {
            ConsultantId = GetRandom(1, 4),
            StartDateTime = new DateTime(year: 2022, month: 1, day: GetRandom(1, 31),
                    hour: GetRandom(9, 16), minute: GetRandomMinute(), second: 0),
            Patient = new Patient()
            {
                FName = "Firstname",
                LName = "Lastname",
                Address1 = "123 Fake Street",
                City = "Citi",
                PostCode = "12345",
            },
        };
        static int GetRandom(int fromInclusive, int toInclusive)
        {
            return RandomNumberGenerator.GetInt32(fromInclusive, toExclusive: toInclusive + 1);
        }
        static int GetRandomMinute()
        {
            var result = GetRandom(0, 1);

            return result == 1 ? 0 : 30;
        }
    }
}