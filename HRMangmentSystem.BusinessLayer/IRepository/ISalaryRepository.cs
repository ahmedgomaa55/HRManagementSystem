using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.BusinessLayer.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.IRepository
{
    public interface ISalaryRepository
    {
        public int CalculatyHolidays(DateOnly from, DateOnly to);
        public List<SalaryReportData> CalculateSalary(string? emplName, DateOnly from);
        public double CalculateNetSalary(string empNationalId, DateOnly from);
        public int calcualateNoOfWorkingHourPerEmp(Employee emp);
    }
}
