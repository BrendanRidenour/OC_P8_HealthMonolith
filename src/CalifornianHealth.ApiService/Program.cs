using CalifornianHealth.Concurrency;
using CalifornianHealth.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Internal;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<ISystemClock, SystemClock>();
builder.Services.AddTransient<IConcurrencyService, ConcurrentQueueConcurrencyService>();

builder.Services.AddDbContext<CHDBContext>(db =>
{
    db.UseSqlServer(builder.Configuration["Data:SqlServer:ConnectionString"]);
});

builder.Services.AddTransient<IFetchConsultantsOperation, EntityFrameworkFetchConsultantsOperation>();
builder.Services.AddTransient<IFetchConsultantCalendarOperation, EntityFrameworkFetchConsultantCalendarOperation>();
builder.Services.AddTransient<IFetchConsultantScheduleOperation, EntityFrameworkFetchConsultantScheduleOperation>();
builder.Services.AddTransient<ICreateAppointmentOperation, EntityFrameworkCreateAppointmentOperation>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();