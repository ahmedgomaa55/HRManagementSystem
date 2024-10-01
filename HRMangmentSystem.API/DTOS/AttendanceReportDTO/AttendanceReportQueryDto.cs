using HRManagementSystem.DataAccessLayer.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HRMangmentSystem.API.DTOS.AttendanceReportDTO
{
    public class AttendanceReportQueryDto
    {
        public int Id { get; set; }
        public string EmployeeNationalId { get; set; }
        public string EmployeeName { get; set; }
        public string DepartmentName { get; set; }
        public DateOnly? AttendanceDate { get; set; }
        public TimeOnly? ArrivalTime { get; set; }
        public TimeOnly? DepartureTime { get; set; }

    }
}
