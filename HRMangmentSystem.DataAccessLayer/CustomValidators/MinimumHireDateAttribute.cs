using System;
using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.DataAccessLayer.CustomValidators
{
    public class MinimumHireDateAttribute : ValidationAttribute
    {
        private readonly DateTime _minimumHireDate;

        public MinimumHireDateAttribute()
        {
            _minimumHireDate = new DateTime(2008, 1, 1);
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime hireDate)
            {
                if (hireDate.Date >= _minimumHireDate)
                {
                    return ValidationResult.Success;
                }
                else
                {
                    return new ValidationResult($"Hire date must be on or after {_minimumHireDate.ToShortDateString()}.");
                }
            }

            return new ValidationResult("Invalid hire date.");
        }
    }
}
