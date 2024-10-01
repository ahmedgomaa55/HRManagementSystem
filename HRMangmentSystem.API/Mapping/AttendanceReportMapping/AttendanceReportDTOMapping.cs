using AutoMapper;
using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.API.DTOS.AnnualHolidaysDTO;
using HRMangmentSystem.API.DTOS.AttendanceReportDTO;
using HRMangmentSystem.API.DTOS.DepartmentDTO;
using HRMangmentSystem.DataAccessLayer.Models;

namespace HRMangmentSystem.API.Mapping.AttendanceReportMapping
{
    public class AttendanceReportDTOMapping : Profile
    {
        public AttendanceReportDTOMapping()
        {
            CreateMap<AttendanceRecord, AttendanceReportQueryDto>()
            .ForMember(dest => dest.EmployeeName, opt => opt.MapFrom(src => src.Employee.Name))
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Employee.Department.Name));


            CreateMap<AttendanceReportCommandDto, AttendanceRecord>()
                .ForMember(dest => dest.AttendanceDate, opt => opt.MapFrom(src => DateOnly.Parse(src.AttendanceDate)))
                .ForMember(dest => dest.ArrivalTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.ArrivalTime)))
                .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.DepartureTime)))
                ;
        }
    }
}
