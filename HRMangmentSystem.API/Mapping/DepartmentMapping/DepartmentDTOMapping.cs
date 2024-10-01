using AutoMapper;
using HRMangmentSystem.API.DTOS.DepartmentDTO;
using HRMangmentSystem.DataAccessLayer.Models;

namespace HRMangmentSystem.API.Mapping.DepartmentMapping
{
    public class DepartmentDTOMapping : Profile
    {
        public DepartmentDTOMapping()
        {
            CreateMap<Department, DepartmentQueryDTO>();
            CreateMap<DepartmentCommandDTO, Department>();
        }
    }
}
