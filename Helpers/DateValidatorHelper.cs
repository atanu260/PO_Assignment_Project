using System.ComponentModel.DataAnnotations;

namespace PO_Assignment_Project.Helpers
{
    public class DateValidatorHelperAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                DateTime enteredDate = (DateTime)value;

                if (enteredDate < DateTime.Today)
                {
                    return new ValidationResult("The date cannot be in the past.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
