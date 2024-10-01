using AutoMapper;
using HRMangmentSystem.API.DTOS.DepartmentDTO;
using HRMangmentSystem.API.ResponseBase;
using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMangmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class DepartmentsController : ControllerBase
    {
        #region Fields
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        #endregion

        #region Ctor
        public DepartmentsController(IDepartmentRepository departmentRepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }
        #endregion

        #region Methods
        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            dynamic response;
            var departments = _departmentRepository.GetTableAsTracking();
            departments = departments.Where(dep => dep.IsDeleted == false).ToList();
            var mappedDepartments = _mapper.Map<List<Department>, List<DepartmentQueryDTO>>(departments);
            if (departments == null)
            {
                response = _responseHandler.NotFound<string>("No Departments Found");
                return NotFound(response);
            }
            response = _responseHandler.Success<List<DepartmentQueryDTO>>(mappedDepartments);
            return Ok(response);
        }
        [HttpGet("GetDepartmentById/{id:int}")]
        public async Task<IActionResult> GetDepartmentById(int id)
        {
            dynamic response;
            Department department = await _departmentRepository.GetByIdAsync(id);
            if (department == null || department.IsDeleted)
            {
                response = _responseHandler.NotFound<string>("No Department Found");
                return NotFound(response);
            }
            response = _responseHandler.Success<DepartmentQueryDTO>(_mapper.Map<Department, DepartmentQueryDTO>(department));
            return Ok(response);
        }
        [HttpPost("CreateDepartment")]
        public async Task<IActionResult> CreateDepartment(DepartmentCommandDTO department)
        {
            dynamic response;
            if (ModelState.IsValid)
            {
                Department mappedDepartment = _mapper.Map<DepartmentCommandDTO, Department>(department);
                mappedDepartment.IsDeleted = false;
                await _departmentRepository.AddAsync(mappedDepartment);
                response = _responseHandler.Success<string>("Department Created Successfully");
                return Ok(response);
            }
            response = _responseHandler.BadRequest<string>("Invalid Data");
            return BadRequest(response);
        }
        [HttpPut("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentCommandDTO department)
        {
            dynamic response;
            if (ModelState.IsValid)
            {
                Department mappedDepartment = _mapper.Map<DepartmentCommandDTO, Department>(department);
                await _departmentRepository.UpdateAsync(mappedDepartment);
                response = _responseHandler.Success<string>("Department Updated Successfully");
                return Ok(response);
            }
            response = _responseHandler.BadRequest<string>("Invalid Data");
            return BadRequest(response);
        }
        [HttpDelete("DeleteDepartment/{id:int}")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            dynamic response;
            Department department = await _departmentRepository.GetByIdAsync(id);
            if (department == null || department.IsDeleted)
            {
                response = _responseHandler.NotFound<string>("No Department Found");
                return NotFound(response);
            }
            department.IsDeleted = true;
            await _departmentRepository.UpdateAsync(department);
            response = _responseHandler.Success<string>("Department Deleted Successfully");
            return Ok(response);
        }
        #endregion
    }
}
