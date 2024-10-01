using System;
using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.DataAccessLayer.CustomValidators
{
    public class MinimumAgeAttribute : ValidationAttribute
    {
        private readonly int _minimumAge;

        public MinimumAgeAttribute(int minimumAge)
        {
            _minimumAge = minimumAge;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                if (dateOfBirth.AddYears(_minimumAge) <= DateTime.Today)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"You must be at least {_minimumAge} years old.");
                }
            }

            return new ValidationResult("Invalid date of birth.");
        }
    }
}
