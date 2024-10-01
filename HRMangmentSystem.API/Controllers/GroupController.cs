using AutoMapper;
using HRMangmentSystem.API.DTOS.AccountDTO;
using HRMangmentSystem.API.DTOS.GroupDTO;
using HRMangmentSystem.API.ResponseBase;
using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMangmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin")]

    public class GroupController : ControllerBase
    {
        private readonly IGroupRepository _groupRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        public GroupController(IGroupRepository groupRepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _groupRepository = groupRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }
        [HttpGet("GetAllGroups")]
        public async Task<IActionResult> GetAllGroups()
        {
            var groups = _groupRepository.GetTableAsTracking();
            var mappedGroups = _mapper.Map<List<Group>, List<GroupQueryDTO>>(groups);
            Response<List<GroupQueryDTO>> response = _responseHandler.Success<List<GroupQueryDTO>>(mappedGroups);
            return Ok(response);
        }
        [HttpGet("GetGroupById/{id:int}")]
        public async Task<IActionResult> GetGroupById(int id)
        {
            var group = await _groupRepository.GetGroupById(id);
            if (group == null)
            {
                Response<string> response = _responseHandler.NotFound<string>("No Group Found");
                return NotFound(response);
            }
            return Ok(group);
        }
        [HttpPost("CreateGroup")]
        public async Task<IActionResult> CreateGroup(GroupCommandDTO group)
        {
            var mappedGroup = _mapper.Map<GroupCommandDTO, Group>(group);
            await _groupRepository.AddAsync(mappedGroup);

            Response<string> response = _responseHandler.Success<string>("Group Created Successfully");
            return Ok(response);
        }
    }
}
