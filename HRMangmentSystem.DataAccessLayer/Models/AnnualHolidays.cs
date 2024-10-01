using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.DataAccessLayer.Models
{
    public class AnnualHolidays
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "You must Enter The Holiday Name")]
        public string HolidayName { get; set; }
        public DateOnly HolidayDate { get; set; }
    }
}
