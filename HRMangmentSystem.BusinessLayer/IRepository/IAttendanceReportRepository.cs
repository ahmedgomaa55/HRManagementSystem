using HRMangmentSystem.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.IRepository
{
    public interface IAttendanceReportRepository : IGenericRepositoryAsync<AttendanceRecord>
    {
        Task AddRangeAsync(List<AttendanceRecord> entities);
        List<AttendanceRecord> GetWithFilter(string EmpNameOrDeptName, DateOnly FromDate, DateOnly ToDate);
        List<AttendanceRecord> GetAttendanceInRange(DateOnly FromDate, DateOnly ToDate);
    }
}
