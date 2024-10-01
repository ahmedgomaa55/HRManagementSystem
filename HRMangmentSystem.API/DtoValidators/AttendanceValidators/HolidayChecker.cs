using HRMangmentSystem.DataAccessLayer.Context;
using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.API.DtoValidators.AttendanceValidators
{
    public class HolidayChecker: ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var _hRMangmentCotext = (HRMangmentCotext)validationContext.GetService(typeof(HRMangmentCotext));

            if (value is string date)
            {
                var attendancedate= DateOnly.Parse(date);
                var holiday = _hRMangmentCotext.AnnualHolidays.FirstOrDefault(x => x.HolidayDate == attendancedate);
                var settings = _hRMangmentCotext.GeneralSettings.FirstOrDefault();
                if (settings == null)
                {
                    return new ValidationResult("The date is a holiday.");
                }

                var holidayday1 = settings.WeeklyHoliday1;
                var holidayday2 = settings.WeeklyHoliday2;
                DayOfWeek firstDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), holidayday1);
                DayOfWeek secondDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), holidayday2);



                if (holiday != null)
                {
                    return new ValidationResult("The date is a holiday.");
                }


                if (attendancedate.DayOfWeek == firstDayOfWeek || attendancedate.DayOfWeek == secondDayOfWeek)
                {
                    return new ValidationResult("The date is a weekend holiday.");
                }
            }

            return ValidationResult.Success;
        }
    }
}
