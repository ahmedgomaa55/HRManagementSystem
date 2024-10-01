using HRMangmentSystem.API.DtoValidators.AttendanceValidators;
using HRMangmentSystem.DataAccessLayer.CustomValidators;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace HRMangmentSystem.API.DTOS.AttendanceReportDTO
{
    public class AttendanceReportCommandDto
    {
        [JsonIgnore]
        public int? Id { get; set; }
        public string EmployeeNationalId { get; set; }
        [HolidayChecker]
        public string AttendanceDate { get; set; }
        //[ArrivalAndDepartureTime]
        public string? ArrivalTime { get; set; }
        //[ArrivalAndDepartureTime]
        public string? DepartureTime { get; set; }
        [JsonIgnore]
        public int? LateHours { get; set; }
        [JsonIgnore]
        public int? EarlyLeaveHours { get; set; }
        [JsonIgnore]
        public int? OvertimeHours { get; set; }

    }
}
