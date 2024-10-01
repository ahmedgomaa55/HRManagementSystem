using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Context;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.Repository
{
    public class AnnualHolidaysRepository : GenericRepositoryAsync<AnnualHolidays>, IAnnualHolidaysRepository
    {
        private readonly DbSet<AnnualHolidays> _holiday;
        public AnnualHolidaysRepository(HRMangmentCotext dbContext) : base(dbContext)
        {
            _holiday = dbContext.Set<AnnualHolidays>();
        }

        public List<DateOnly> FilterByDate(DateOnly from, DateOnly to)
        {
            return _holiday.Where(x => x.HolidayDate.CompareTo(from) >= 0 && x.HolidayDate.CompareTo(to) <= 0).Select(x => x.HolidayDate).ToList();
        }
    }
}
