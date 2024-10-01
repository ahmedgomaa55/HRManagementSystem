using HRMangmentSystem.API.DTOS.AttendanceReportDTO;
using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.API.DtoValidators.AttendanceValidators
{
    public class ArrivalAndDepartureTimeAttribute : ValidationAttribute
    {
        public ArrivalAndDepartureTimeAttribute()
        {

        }
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is string time)
            {
                if (time.Length == 5)
                {
                    if (time[2] == ':')
                    {
                        return ValidationResult.Success;
                    }
                    else
                    {
                        return new ValidationResult("Invalid Time Format");
                    }
                }
                else
                {
                    return new ValidationResult("Invalid Time Format");
                }
            }
            return new ValidationResult("Invalid Time Format");
        }
    }
}

