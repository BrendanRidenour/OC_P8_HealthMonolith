using CalifornianHealth.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<CHDBContext>(db =>
{
    db.UseSqlServer(builder.Configuration["Data:SqlServer:ConnectionString"]);
});

builder.Services.AddTransient<IFetchConsultantsOperation, EntityFrameworkOperationService>();
builder.Services.AddTransient<IFetchConsultantCalendarOperation, EntityFrameworkOperationService>();
builder.Services.AddTransient<IFetchConsultantScheduleOperation, EntityFrameworkOperationService>();
builder.Services.AddTransient<ICreateAppointmentOperation, EntityFrameworkOperationService>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();