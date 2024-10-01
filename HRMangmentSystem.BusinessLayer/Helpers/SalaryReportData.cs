using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.Helpers
{
    public class SalaryReportData
    {
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public double BaseSalary { get; set; }
        public int TotalAttendanceDays { get; set; }
        public int TotalAbsentDays { get; set; }
        public int TotalOvertimeHours { get; set; }
        public int PenalityHours { get; set; }
        public double TotalPenality { get; set; }
        public double TotalSalary { get; set; }
        public double TotalBounus { get; set; }
    }
}
