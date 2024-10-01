using AutoMapper;
using HRMangmentSystem.API.DTOS.SalaryReportDTO;
using HRMangmentSystem.BusinessLayer.Helpers;

namespace HRMangmentSystem.API.Mapping.SalaryReportMapping
{
    public class SalaryReportDtoMapping : Profile
    {
        public SalaryReportDtoMapping()
        {
            CreateMap<SalaryReportData, SalaryReportQueryDTO>();
        }
    }
}
