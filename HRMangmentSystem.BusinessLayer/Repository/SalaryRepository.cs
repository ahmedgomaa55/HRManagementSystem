using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.BusinessLayer.Helpers;
using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Models;

namespace HRMangmentSystem.BusinessLayer.Repository
{
    public class SalaryRepository : ISalaryRepository
    {
        private readonly ISettingsRepository _settingsRepository;
        private readonly IAnnualHolidaysRepository _annualHolidaysRepository;
        private readonly IAttendanceReportRepository _attendanceReportRepository;
        private readonly IEmployeeRepository _employeeRepository;
        public SalaryRepository(ISettingsRepository settingsRepository, IAnnualHolidaysRepository annualHolidaysRepository, IAttendanceReportRepository attendanceReportRepository, IEmployeeRepository employeeRepository)
        {
            _settingsRepository = settingsRepository;
            _annualHolidaysRepository = annualHolidaysRepository;
            _attendanceReportRepository = attendanceReportRepository;
            _employeeRepository = employeeRepository;
        }
        public int CalculatyHolidays(DateOnly from, DateOnly to)
        {
            int totalHolidays = 0;
            List<DateOnly> annualHolidays = _annualHolidaysRepository.FilterByDate(from, to);
            var settingData = _settingsRepository.GetTableAsTracking().FirstOrDefault();
            string firstDay = settingData.WeeklyHoliday1;
            string secondDay = settingData.WeeklyHoliday2;
            DayOfWeek firstDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), firstDay);
            DayOfWeek secondDayOfWeek = (DayOfWeek)Enum.Parse(typeof(DayOfWeek), secondDay);


            foreach (var holiday in annualHolidays)
            {
                if (holiday.DayOfWeek == firstDayOfWeek || holiday.DayOfWeek == secondDayOfWeek)
                {

                }
                else
                {
                    totalHolidays++;
                }
            }
            for (DateOnly date = from; date <= to; date = date.AddDays(1))
            {
                if (date.DayOfWeek == firstDayOfWeek || date.DayOfWeek == secondDayOfWeek)
                {
                    totalHolidays++;
                }
            }
            return totalHolidays;
        }
        public int CalculateTotalWorkingDays(string empName, DateOnly from)
        {
            int totalWorkingDays = DateTime.DaysInMonth(from.Year, from.Month);
            var attendanceData = _attendanceReportRepository.GetWithFilter(empName, from, from.AddDays(totalWorkingDays));
            int totalAttendanceDays = attendanceData.Count;
            return totalAttendanceDays;
        }
        public async Task<int> CalculateAbsent(string empNationalId, DateOnly from)
        {
            int totalAbsent = 0;
            Employee employee = await _employeeRepository.GetEmployeeByNationalId(empNationalId);
            int totalWorkingDays = DateTime.DaysInMonth(from.Year, from.Month);
            totalAbsent = totalWorkingDays - CalculateTotalWorkingDays(employee.Name, from) - CalculatyHolidays(from, from.AddDays(totalWorkingDays));
            return totalAbsent;
        }
        public async Task<int> CalculateLate(string empNationalId, DateOnly from)
        {
            int totalLate = 0;
            Employee employee = await _employeeRepository.GetEmployeeByNationalId(empNationalId);
            var attendanceData = _attendanceReportRepository.GetWithFilter(employee.Name, from, from.AddDays(DateTime.DaysInMonth(from.Year, from.Month)));
            foreach (var attendance in attendanceData)
            {
                if (attendance.LateHours > 0 && attendance.LateHours is not null)
                {
                    totalLate += attendance.LateHours.Value;
                }
            }
            return totalLate;
        }
        public async Task<int> CalculateEarlyLeave(string empNationalId, DateOnly from)
        {
            int totalEarlyLeave = 0;
            Employee employee = await _employeeRepository.GetEmployeeByNationalId(empNationalId);
            var attendanceData = _attendanceReportRepository.GetWithFilter(employee.Name, from, from.AddDays(DateTime.DaysInMonth(from.Year, from.Month)));
            foreach (var attendance in attendanceData)
            {
                if (attendance.EarlyLeaveHours > 0 && attendance.EarlyLeaveHours is not null)
                {
                    totalEarlyLeave += attendance.EarlyLeaveHours.Value;
                }
            }
            return totalEarlyLeave;
        }
        public async Task<int> CalculateOvertime(string empNationalId, DateOnly from)
        {
            int totalOvertime = 0;
            Employee employee = await _employeeRepository.GetEmployeeByNationalId(empNationalId);
            var attendanceData = _attendanceReportRepository.GetWithFilter(employee.Name, from, from.AddDays(DateTime.DaysInMonth(from.Year, from.Month)));
            foreach (var attendance in attendanceData)
            {
                if (attendance.OvertimeHours > 0 && attendance.OvertimeHours is not null)
                {
                    totalOvertime += attendance.OvertimeHours.Value;
                }
            }
            return totalOvertime;
        }
        public int calcualateNoOfWorkingHourPerEmp(Employee emp)
        {
            return (int)(emp.DepartureTime - emp.AttendanceTime).TotalHours;
        }
        public double CalculateNetSalary(string empNationalId, DateOnly from)
        {

            double netSalary = 0;
            Employee employee = _employeeRepository.GetEmployeeByNationalId(empNationalId).Result;
            List<GeneralSettings> setting = _settingsRepository.GetTableAsTracking();
            decimal PenaltyRate = setting[0].PenaltyRate;
            decimal BonusRate = setting[0].BonusRate;
            double baseSalary = employee.Salary;
            int totalAbsent = CalculateAbsent(empNationalId, from).Result;
            int totalLate = CalculateLate(empNationalId, from).Result;
            int totalEarlyLeave = CalculateEarlyLeave(empNationalId, from).Result;
            int totalOvertime = CalculateOvertime(empNationalId, from).Result;
            int totalWorkingDays = DateTime.DaysInMonth(from.Year, from.Month);
            int totalHolidays = CalculatyHolidays(from, from.AddDays(totalWorkingDays));
            netSalary = baseSalary -
                (totalAbsent * calcualateNoOfWorkingHourPerEmp(employee) * (double)(PenaltyRate / 100) * baseSalary) -
                (totalLate * (double)(PenaltyRate / 100) * baseSalary) -
                (totalEarlyLeave * (double)(PenaltyRate / 100) * baseSalary) +
                (totalOvertime * (double)(BonusRate / 100) * baseSalary);
            return netSalary > 0 ? netSalary : 0;
        }
        public double CalculateTotalBonus(int hours, double salary, decimal bonusRate)
        {
            return hours * salary * (double)(bonusRate / 100);
        }
        public double CalculateTotalPenality(int hours, double salary, decimal penalityRate)
        {
            return hours * salary * (double)(penalityRate / 100);
        }
        public List<SalaryReportData> CalculateSalary(string? emplName, DateOnly from)
        {
            var Data = new List<SalaryReportData>();
            var settings = _settingsRepository.GetTableAsTracking();
            decimal bonusRate = settings[0].BonusRate;
            decimal penalityRate = settings[0].PenaltyRate;
            if (emplName == null || emplName == "")
            {
                var allEmps = _employeeRepository.GetTableAsTracking();
                foreach (var emp in allEmps)
                {
                    if (emp.IsDeleted == false)
                    {
                        SalaryReportData salaryReportData = new SalaryReportData();
                        salaryReportData.EmployeeName = emp.Name;
                        salaryReportData.BaseSalary = emp.Salary;
                        salaryReportData.DepartmentName = emp.Department.Name;
                        salaryReportData.TotalAbsentDays = CalculateAbsent(emp.NationalId, from).Result;
                        salaryReportData.PenalityHours = CalculateLate(emp.NationalId, from).Result + CalculateEarlyLeave(emp.NationalId, from).Result;
                        salaryReportData.TotalOvertimeHours = CalculateOvertime(emp.NationalId, from).Result;
                        salaryReportData.TotalSalary = CalculateNetSalary(emp.NationalId, from);
                        salaryReportData.TotalAttendanceDays = CalculateTotalWorkingDays(emp.Name, from);
                        salaryReportData.TotalBounus = CalculateTotalBonus(CalculateOvertime(emp.NationalId, from).Result, emp.Salary, bonusRate);
                        salaryReportData.TotalPenality = CalculateTotalPenality(CalculateLate(emp.NationalId, from).Result + CalculateEarlyLeave(emp.NationalId, from).Result + (CalculateAbsent(emp.NationalId, from).Result * calcualateNoOfWorkingHourPerEmp(emp)), emp.Salary, penalityRate);
                        Data.Add(salaryReportData);

                    }
                }
            }
            else
            {
                List<Employee> employee = _employeeRepository.GetEmployeeByName(emplName);
                foreach (var emp in employee)
                {
                    if (emp.IsDeleted == false)
                    {
                        SalaryReportData salaryReportData = new SalaryReportData();
                        salaryReportData.EmployeeName = emp.Name;
                        salaryReportData.BaseSalary = emp.Salary;
                        salaryReportData.DepartmentName = emp.Department.Name;
                        salaryReportData.TotalAbsentDays = CalculateAbsent(emp.NationalId, from).Result;
                        salaryReportData.PenalityHours = CalculateLate(emp.NationalId, from).Result + CalculateEarlyLeave(emp.NationalId, from).Result;
                        salaryReportData.TotalOvertimeHours = CalculateOvertime(emp.NationalId, from).Result;
                        salaryReportData.TotalSalary = CalculateNetSalary(emp.NationalId, from);
                        salaryReportData.TotalAttendanceDays = CalculateTotalWorkingDays(emp.Name, from);
                        salaryReportData.TotalBounus = CalculateTotalBonus(CalculateOvertime(emp.NationalId, from).Result, emp.Salary, bonusRate);
                        salaryReportData.TotalPenality = CalculateTotalPenality(CalculateLate(emp.NationalId, from).Result + CalculateEarlyLeave(emp.NationalId, from).Result + (CalculateAbsent(emp.NationalId, from).Result * calcualateNoOfWorkingHourPerEmp(emp)), emp.Salary, penalityRate);
                        Data.Add(salaryReportData);
                    }
                }

            }
            return Data;
        }

    }
}
