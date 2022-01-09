using Microsoft.Extensions.Internal;
using System.ComponentModel.DataAnnotations;

namespace CalifornianHealth.Validation
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = false)]
    public sealed class DateTimeAfterAttribute : ValidationAttribute
    {
        public int ValidMinutesAfterNow { get; set; }

        public DateTimeAfterAttribute(int validMinutesAfterNow)
        {
            this.ValidMinutesAfterNow = validMinutesAfterNow;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is null)
                return ValidationResult.Success;

            if (!DateTime.TryParse(value.ToString(), out DateTime dateTime))
                return new ValidationResult("Please enter a valid DateTime.");

            var clock = validationContext.GetService(typeof(ISystemClock)) as ISystemClock;

            if (clock is null)
                throw new InvalidOperationException($"This validation requires an {nameof(ISystemClock)}. Make sure the service is registered with the services attached to the {nameof(ValidationContext)}.");

            var now = clock.UtcNow.LocalDateTime;
            var validAfter = now.AddMinutes(this.ValidMinutesAfterNow);

            if (dateTime < validAfter)
                return new ValidationResult($"This value won't be valid until after: '{validAfter}'");

            return ValidationResult.Success;
        }
    }
}