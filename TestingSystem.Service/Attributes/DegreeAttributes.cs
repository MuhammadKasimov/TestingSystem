using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace TestingSystem.Service.Attributes
{
    public class DegreeAttributes : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string degree &&
                (degree.Length == 1 || degree.Length == 2) &&
                degree.Any(c => char.IsDigit(c)) &&
                int.Parse(degree) >= 1 &&
                int.Parse(degree) <= 11)
            {
                return ValidationResult.Success;
            }
            return new ValidationResult("Password should contain at least 8 characters," +
                                                    " should contain at least on letter and digit");
        }
    }
}
