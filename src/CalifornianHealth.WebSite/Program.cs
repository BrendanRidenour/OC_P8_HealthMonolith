using CalifornianHealth.Data;

var builder = WebApplication.CreateBuilder(args);

var apiServiceOptions = new ApiServiceClientOptions(
    baseAddress: builder.Configuration["ApiServiceClient:BaseAddress"]);
builder.Services.AddSingleton(apiServiceOptions);

builder.Services.AddHttpClient();

builder.Services.AddTransient<IFetchConsultantsOperation, ApiServiceClient>();
builder.Services.AddTransient<IFetchConsultantCalendarOperation, ApiServiceClient>();
builder.Services.AddTransient<IFetchConsultantScheduleOperation, ApiServiceClient>();
builder.Services.AddTransient<ICreateAppointmentOperation, ApiServiceClient>();

builder.Services.AddControllersWithViews();

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