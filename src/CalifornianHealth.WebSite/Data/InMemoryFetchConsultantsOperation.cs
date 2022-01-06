namespace CalifornianHealth.Data
{
    public class InMemoryFetchConsultantsOperation : IFetchConsultantsOperation
    {
        public Task<IReadOnlyList<Consultant>> FetchConsultants()
        {
            var list = new List<Consultant>()
            {
                new Consultant()
                {
                    Id = 1,
                    FName = "Jessica",
                    LName = "Wally",
                    Speciality = "Cariologist",
                },
                new Consultant()
                {
                    Id = 2,
                    FName = "Iai",
                    LName = "Donnas",
                    Speciality = "General Surgeon",
                },
                new Consultant()
                {
                    Id = 3,
                    FName = "Amanda",
                    LName = "Denyl",
                    Speciality = "Doctor",
                },
                new Consultant()
                {
                    Id = 4,
                    FName = "Jason",
                    LName = "Davis",
                    Speciality = "Cardiologist",
                },
            };

            return Task.FromResult<IReadOnlyList<Consultant>>(list.AsReadOnly());
        }
    }
}