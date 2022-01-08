namespace CalifornianHealth.Data
{
    public class ApiServiceClientOptions
    {
        public string ApiRootEndpoint { get; }

        public ApiServiceClientOptions(string apiRootEndpoint)
        {
            if (string.IsNullOrWhiteSpace(apiRootEndpoint))
            {
                throw new ArgumentException($"'{nameof(apiRootEndpoint)}' cannot be null or whitespace.", nameof(apiRootEndpoint));
            }

            this.ApiRootEndpoint = apiRootEndpoint;
        }
    }
}