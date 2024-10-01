using AutoMapper;
using HRMangmentSystem.API.DTOS.AccountDTO;
using HRMangmentSystem.DataAccessLayer.Models;

namespace HRMangmentSystem.API.Mapping.AccountMapping
{
    public class AccountDTOMapping : Profile
    {
        public AccountDTOMapping()
        {
            CreateMap<AccountCommandDTO, ApplicationUser>();
        }
    }
}
