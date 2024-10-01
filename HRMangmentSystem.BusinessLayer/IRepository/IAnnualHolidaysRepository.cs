using HRMangmentSystem.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.IRepository
{
    public interface IAnnualHolidaysRepository : IGenericRepositoryAsync<AnnualHolidays>
    {
        List<DateOnly> FilterByDate(DateOnly from, DateOnly to);
    }
}
