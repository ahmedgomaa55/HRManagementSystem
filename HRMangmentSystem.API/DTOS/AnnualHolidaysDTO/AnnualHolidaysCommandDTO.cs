using System.ComponentModel.DataAnnotations;

namespace HRMangmentSystem.API.DTOS.AnnualHolidaysDTO
{
    public class AnnualHolidaysCommandDTO
    {
        public int? Id { get; set; }
        [Required(ErrorMessage = "You must Enter The Holiday Name")]
        public string HolidayName { get; set; }
        public string HolidayDate { get; set; }
    }
}
