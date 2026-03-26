using System.ComponentModel.DataAnnotations;
using System.Globalization;

namespace MakeTopGreatAgain.Attributes
{
    public class afterDate(String max): ValidationAttribute("Date {0} must be not too late than {1}")
    {
        public DateTime Max = DateTime.Parse(max);

        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return true;
            }
            if (value is not DateTime DTVal)
            {
                throw new InvalidCastException("Cannot cast to int");
            }
            return DTVal < Max;
        }
        public override string FormatErrorMessage(string name) => string.Format(CultureInfo.CurrentCulture, ErrorMessageString, name, Max);
    }
}
