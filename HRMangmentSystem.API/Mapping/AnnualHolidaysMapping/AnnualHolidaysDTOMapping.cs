using AutoMapper;
using HRMangmentSystem.API.DTOS.AnnualHolidaysDTO;
using HRMangmentSystem.DataAccessLayer.Models;

namespace HRMangmentSystem.API.Mapping.AnnualHolidaysMapping
{
    public class AnnualHolidaysDTOMapping : Profile
    {
        public AnnualHolidaysDTOMapping()
        {
            CreateMap<AnnualHolidaysCommandDTO, AnnualHolidays>()
                .ForMember(dest => dest.HolidayDate, opt => opt.MapFrom(src => DateOnly.Parse(src.HolidayDate)))
                ;

            CreateMap<AnnualHolidays, AnnualHolidaysQueryDTO>();
        }
    }
}
