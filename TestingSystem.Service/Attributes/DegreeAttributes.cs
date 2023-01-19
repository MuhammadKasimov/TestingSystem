using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TestingSystem.Service.Attributes
{
    public class DegreeAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string degree)
            {
                if (string.IsNullOrEmpty(degree))
                    return ValidationResult.Success;

                else if ((degree.Length == 1 || degree.Length == 2) && degree.Any(c => char.IsDigit(c)) &&
                int.Parse(degree) >= 1 && int.Parse(degree) <= 11)
                {
                    return ValidationResult.Success;
                }
            }
            else if (value is null)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("degree should be only number from 1 to 11");
        }
    }
}
