namespace CalifornianHealth
{
    public class Consultant
    {
        public int Id { get; set; }
        public string FName { get; set; } = null!;
        public string LName { get; set; } = null!;
        public string Speciality { get; set; } = null!;

        public override string ToString() => $"{FName} {LName}, {Speciality}".Trim();
    }
}