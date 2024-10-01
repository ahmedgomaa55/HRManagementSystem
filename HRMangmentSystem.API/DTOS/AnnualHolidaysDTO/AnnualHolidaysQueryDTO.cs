using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.API.DTOS.AnnualHolidaysDTO
{
    public class AnnualHolidaysQueryDTO
    {
        public int Id { get; set; }
        public string HolidayName { get; set; }
        public DateOnly HolidayDate { get; set; }
    }
}
