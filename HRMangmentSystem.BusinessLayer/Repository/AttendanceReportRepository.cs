using HRManagementSystem.DataAccessLayer.Models;
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
    public class AttendanceReportRepository : GenericRepositoryAsync<AttendanceRecord>, IAttendanceReportRepository
    {
        private readonly DbSet<AttendanceRecord> _attendance;
        public AttendanceReportRepository(HRMangmentCotext dbContext) : base(dbContext)
        {
            _attendance = dbContext.Set<AttendanceRecord>();
        }

        public async Task AddRangeAsync(List<AttendanceRecord> entities)
        {
            await _attendance.AddRangeAsync(entities);
            await SaveChangesAsync();
        }

        public List<AttendanceRecord> GetAttendanceInRange(DateOnly FromDate, DateOnly ToDate)
        {
            var query = _attendance.Include(emp => emp.Employee)
                                    .ThenInclude(dept => dept.Department)
                                    .Where(record =>
                                        (record.AttendanceDate.CompareTo(FromDate) >= 0 && record.AttendanceDate.CompareTo(ToDate) <= 0)
                                    )
                                    .ToList();

            return query;
        }

        public override List<AttendanceRecord> GetTableAsTracking()
        {
            return _attendance.Include(emp => emp.Employee).
                ThenInclude(dept => dept.Department).ToList();
        }

        public List<AttendanceRecord> GetWithFilter(string? EmpNameOrDeptName, DateOnly FromDate, DateOnly ToDate)
        {
            var query = _attendance.Include(emp => emp.Employee)
                                    .ThenInclude(dept => dept.Department)
                                    .Where(record =>
                                        (record.Employee.Name.Contains(EmpNameOrDeptName ?? "") ||
                                        record.Employee.Department.Name.Contains(EmpNameOrDeptName))
                                        &&
                                        (record.AttendanceDate.CompareTo(FromDate) >= 0 && record.AttendanceDate.CompareTo(ToDate) <= 0)
                                    )
                                    .ToList();

            return query;
        }
    }
}
