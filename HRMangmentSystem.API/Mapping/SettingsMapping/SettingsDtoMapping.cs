using AutoMapper;
using HRMangmentSystem.API.DTOS.GroupDTO;
using HRMangmentSystem.API.DTOS.PermissionDTO;
using HRMangmentSystem.API.DTOS.SettingsDto;
using HRMangmentSystem.DataAccessLayer.Models;

namespace HRMangmentSystem.API.Mapping.SettingsMapping
{
    public class SettingsDtoMapping:Profile
    {
        public SettingsDtoMapping()
        {
            CreateMap<SettingsCommandDto, GeneralSettings>();

            CreateMap<GeneralSettings, SettingsQueryDto>();

            
        }
    }
}
