using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth
{
    public class Patient
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string FName { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string LName { get; set; } = null!;

        [Required]
        [StringLength(255)]
        public string Address1 { get; set; } = null!;

        [StringLength(255)]
        public string Address2 { get; set; } = null!;

        [Required]
        [StringLength(50)]
        public string City { get; set; } = null!;

        [Required]
        [StringLength(10)]
        public string PostCode { get; set; } = null!;

        public Patient(Patient patient)
            : this()
        {
            this.Id = patient.Id;
            this.FName = patient.FName;
            this.LName = patient.LName;
            this.Address1 = patient.Address1;
            this.Address2 = patient.Address2;
            this.City = patient.City;
            this.PostCode = patient.PostCode;
        }

        public Patient() { }
    }
}