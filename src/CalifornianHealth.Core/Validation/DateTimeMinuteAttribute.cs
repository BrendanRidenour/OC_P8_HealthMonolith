using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CalifornianHealth.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class DateTimeMinuteAttribute : ValidationAttribute
    {
        public int[] ValidValues { get; set; }

        public DateTimeMinuteAttribute(params int[] validValues)
        {
            if (validValues.Length == 0)
                throw new ArgumentException(paramName: nameof(validValues), message: "This collection cannot be empty.");

            this.ValidValues = validValues;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (!DateTime.TryParse(value.ToString(), out DateTime dateTime))
                return new ValidationResult("Please enter a valid DateTime.");

            foreach (var validValue in ValidValues)
                if (dateTime.Minute == validValue)
                    return ValidationResult.Success;

            var errorMessage = new StringBuilder($"This {nameof(DateTime.Minute)} value of this {nameof(DateTime)} must use one of the following value values: ");
            foreach (var validValue in ValidValues)
                errorMessage.Append($"{validValue},");

            return new ValidationResult(errorMessage.ToString().TrimEnd(','));
        }
    }
}