namespace HRMangmentSystem.API.DTOS.SalaryReportDTO
{
    public class SalaryReportQueryDTO
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
