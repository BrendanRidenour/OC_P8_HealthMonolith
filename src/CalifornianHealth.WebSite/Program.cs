using CalifornianHealth.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IFetchConsultantsOperation, InMemoryFetchConsultantsOperation>();
builder.Services.AddTransient<IFetchConsultantCalendarOperation, InMemoryFetchConsultantCalendarOperation>();
builder.Services.AddTransient<ICreateAppointmentOperation, InMemoryCreateAppointmentOperation>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseStatusCodePagesWithReExecute("/Home/StatusCodePage", "?statusCode={0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();