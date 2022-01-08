using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class DateTimeHourAttribute : ValidationAttribute
    {
        public int MinValue { get; set; } = 0;
        public int MaxValue { get; set; } = 23;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (!DateTime.TryParse(value.ToString(), out DateTime dateTime))
                return new ValidationResult("Please enter a valid DateTime.");

            if (dateTime.Hour < this.MinValue || dateTime.Hour > this.MaxValue)
                return new ValidationResult($"The {nameof(DateTime.Hour)} associated with this {nameof(DateTime)} must be no lower than 9 and no higher than 16.");

            return ValidationResult.Success;
        }
    }
}