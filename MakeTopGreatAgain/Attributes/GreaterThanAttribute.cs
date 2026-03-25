using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;

namespace MakeTopGreatAgain.Attributes
{
    public class GreaterThanAttribute(int min): ValidationAttribute
    {
        public int Min { get; } = min;

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }
            if (value is not int intVal)
            {
                return new ValidationResult("Not an int", [validationContext.MemberName]);
            }
            if (intVal <= Min)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Value must be greater than {Min}.",
                    [validationContext.MemberName]
                    );
            }
            return ValidationResult.Success;

        }

    }
}
