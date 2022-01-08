using System.Net;

namespace CalifornianHealth.Data
{
    public class ApiServiceClient
        : IFetchConsultantsOperation, IFetchConsultantCalendarOperation, IFetchConsultantScheduleOperation,
        ICreateAppointmentOperation
    {
        private readonly HttpClient _http;
        private readonly ApiServiceClientOptions _options;

        public ApiServiceClient(HttpClient http, ApiServiceClientOptions options)
        {
            this._http = http ?? throw new ArgumentNullException(nameof(http));
            this._options = options ?? throw new ArgumentNullException(nameof(options));

            this._http.BaseAddress = new Uri(this._options.BaseAddress);
        }

        public async Task<IReadOnlyList<Consultant>?> FetchConsultants()
        {
            var response = await this._http.GetFromJsonAsync<List<Consultant>>("consultants");

            return response?.AsReadOnly();
        }

        public async Task<ConsultantAvailability<Date>?> FetchConsultantCalendar(int consultantId)
        {
            var response = await this._http.GetFromJsonAsync<ConsultantAvailability<Date>?>(
                $"consultants/{consultantId}");

            return response;
        }

        public async Task<ConsultantAvailability<Time>?> FetchConsultantSchedule(int consultantId, Date date)
        {
            var response = await this._http.GetFromJsonAsync<ConsultantAvailability<Time>?>(
                $"consultants/{consultantId}/schedule/{date}");

            return response;
        }

        public async Task<bool> CreateAppointment(Appointment appointment)
        {
            var response = await this._http.PostAsJsonAsync("appointment", appointment);

            if (response.StatusCode == HttpStatusCode.Created)
                return true;

            return false;
        }
    }
}