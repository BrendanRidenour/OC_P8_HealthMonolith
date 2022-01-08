namespace CalifornianHealth.Data
{
    public class ApiServiceClientOptions
    {
        public string BaseAddress { get; }

        public ApiServiceClientOptions(string baseAddress)
        {
            if (string.IsNullOrWhiteSpace(baseAddress))
            {
                throw new ArgumentException($"'{nameof(baseAddress)}' cannot be null or whitespace.", nameof(baseAddress));
            }

            this.BaseAddress = baseAddress;
        }
    }
}