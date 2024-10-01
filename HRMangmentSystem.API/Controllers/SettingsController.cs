using AutoMapper;
using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.API.DTOS.DepartmentDTO;
using HRMangmentSystem.API.DTOS.EmployeeDTO;
using HRMangmentSystem.API.DTOS.SettingsDto;
using HRMangmentSystem.API.ResponseBase;
using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.BusinessLayer.Repository;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMangmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class SettingsController : ControllerBase
    {
        private readonly ISettingsRepository _settingstRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        public SettingsController(ISettingsRepository settingsrepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _settingstRepository = settingsrepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }
        [HttpGet("GetSetting")]
        public async Task<IActionResult> GetSetting()
        {
            dynamic response;
            var settings = _settingstRepository.GetTableAsTracking();
            var mappedSettings = _mapper.Map<List<GeneralSettings>, List<SettingsQueryDto>>(settings);
            if (settings == null)
            {
                response = _responseHandler.NotFound<string>("No Settings Found");
                return NotFound(response);
            }
            response = _responseHandler.Success<List<SettingsQueryDto>>(mappedSettings);
            return Ok(response);
        }
        [HttpPost("CreateSettings")]
        public async Task<IActionResult> SetSetting(SettingsCommandDto settingsCommandDto)
        {
            dynamic response;
            if (ModelState.IsValid)
            {
                var currentSettings = _settingstRepository.GetTableAsTracking();
                if (currentSettings.Count == 0 || currentSettings == null)
                {
                    var mappedSettings = _mapper.Map<SettingsCommandDto, GeneralSettings>(settingsCommandDto);
                    await _settingstRepository.AddAsync(mappedSettings);
                    response = _responseHandler.Success<SettingsCommandDto>(settingsCommandDto);
                    return Ok(response);
                }

            }
            response = _responseHandler.NotFound<string>("No Settings Found");
            return NotFound(response);


        }
        [HttpPut("UpdateSettings")]
        public async Task<IActionResult> UpdateSetting(SettingsCommandDto settingsCommandDto)
        {
            List<GeneralSettings>? currentSettings = _settingstRepository.GetTableAsTracking();




            dynamic response;
            //update
            if (ModelState.IsValid && currentSettings.Count == 1)

            {
                var currentsettingsid = currentSettings[0].Id;
                settingsCommandDto.Id = currentsettingsid;
                var mappedSettings = _mapper.Map<SettingsCommandDto, GeneralSettings>(settingsCommandDto);
                await _settingstRepository.UpdateAsync(mappedSettings);
                response = _responseHandler.Success<SettingsCommandDto>(settingsCommandDto);
                return Ok(response);


            }
            else if (ModelState.IsValid && currentSettings.Count == 0)
            {
                await SetSetting(settingsCommandDto);
                return Ok();

            }
            response = _responseHandler.NotFound<string>("No Settings Found");
            return NotFound(response);


        }
        [HttpDelete("DeleteSettings")]
        public async Task<IActionResult> DeleteSettings(int settingId)
        {
            dynamic response;
            GeneralSettings entity = await _settingstRepository.GetByIdAsync(settingId);
            if (entity == null)
            {
                response = _responseHandler.NotFound<string>("No Settings Found");
                return NotFound(response);
            }
            await _settingstRepository.DeleteAsync(entity);
            response = _responseHandler.Success<string>("Deleted Successfully");
            return Ok(response);

        }
    }
}
