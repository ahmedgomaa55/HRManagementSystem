using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.DataAccessLayer.Models
{
    public class GeneralSettings
    {
        public int Id { get; set; }
        public decimal BonusRate { get; set; }
        public decimal PenaltyRate { get; set; }
        public string WeeklyHoliday1 { get; set; }
        public string? WeeklyHoliday2 { get; set; }
    }
}
