using AutoMapper;
using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.API.DTOS.EmployeeDTO;
using System.ComponentModel;

namespace HRMangmentSystem.API.Mapping.EmployeeMapping
{
    public class EmployeeDTOMapping : Profile
    {
        public EmployeeDTOMapping()
        {
            CreateMap<Employee, EmployeeQueryDTO>()
            .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department.Name))
            ;

            CreateMap<EmployeeCommandDTO, Employee>()
                .ForMember(dest => dest.AttendanceTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.AttendanceTime)))
                .ForMember(dest => dest.DepartureTime, opt => opt.MapFrom(src => TimeOnly.Parse(src.DepartureTime)))
                .ForMember(dest => dest.HireDate, opt => opt.MapFrom(src => DateOnly.Parse(src.HireDate)))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => DateOnly.Parse(src.DateOfBirth)))
                ;
        }

    }

}
