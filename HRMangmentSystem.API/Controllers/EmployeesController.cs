using AutoMapper;
using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.API.DTOS.EmployeeDTO;
using HRMangmentSystem.API.ResponseBase;
using HRMangmentSystem.BusinessLayer.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HRMangmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class EmployeesController : ControllerBase
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ResponseHandler _responseHandler;
        #endregion

        #region Ctor
        public EmployeesController(
            IMapper mapper,
            IEmployeeRepository employeeRepository,
            ResponseHandler responseHandler
            )
        {
            _mapper = mapper;
            _employeeRepository = employeeRepository;
            _responseHandler = responseHandler;
        }
        #endregion

        #region Methods
        [HttpGet("GetAllEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            dynamic response;
            var employees = _employeeRepository.GetTableAsTracking();
            if (employees == null)
            {
                response = _responseHandler.NotFound<string>("No Employees Found");
                return NotFound(response);
            }
            List<EmployeeQueryDTO> mappedEmployees = _mapper.Map<List<Employee>, List<EmployeeQueryDTO>>(employees);
            mappedEmployees = mappedEmployees.Where(emp => emp.IsDeleted == false).ToList();
            response = _responseHandler.Success<List<EmployeeQueryDTO>>(mappedEmployees);
            return Ok(response);

        }
        [HttpGet("GetEmployeeByNationalId")]
        public async Task<IActionResult> GetEmployeeByNationalId(string NationalId)
        {
            dynamic response;
            var employee = await _employeeRepository.GetEmployeeByNationalId(NationalId);
            EmployeeQueryDTO mappedEmployee = _mapper.Map<Employee, EmployeeQueryDTO>(employee);
            if (employee == null || mappedEmployee.IsDeleted)
            {
                response = _responseHandler.NotFound<string>("No Employee Found");
                return NotFound(response);
            }
            response = _responseHandler.Success<EmployeeQueryDTO>(mappedEmployee);
            return Ok(mappedEmployee);
        }

        [HttpPost("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeCommandDTO employeeCommandDTO)
        {
            dynamic response;
            if (ModelState.IsValid)
            {
                Employee employee = _mapper.Map<EmployeeCommandDTO, Employee>(employeeCommandDTO);
                employee.IsDeleted = false;
                await _employeeRepository.AddAsync(employee);
                response = _responseHandler.Success<string>($"Employee {employee.Name} Added Successfully");
                return Ok(response);
            }
            response = _responseHandler.BadRequest<string>("Invalid Data");
            return BadRequest(response);
        }
        [HttpPut("UpdateEmployee")]
        public async Task<IActionResult> UpdateEmployee(EmployeeCommandDTO employeeCommandDTO)
        {
            dynamic response;
            if (ModelState.IsValid)
            {
                Employee employee = _mapper.Map<EmployeeCommandDTO, Employee>(employeeCommandDTO);
                await _employeeRepository.UpdateAsync(employee);
                response = _responseHandler.Success<string>($"Employee {employee.Name} Updated Successfully");
                return Ok(response);
            }
            response = _responseHandler.BadRequest<string>("Invalid Data");
            return BadRequest(response);
        }
        [HttpDelete("DeleteEmployee")]
        public async Task<IActionResult> DeleteEmployee(string NationalId)
        {
            dynamic response;
            var employee = await _employeeRepository.GetEmployeeByNationalId(NationalId);
            if (employee == null)
            {
                response = _responseHandler.NotFound<string>("No Employee Found");
                return NotFound(response);
            }
            employee.IsDeleted = true;
            await _employeeRepository.UpdateAsync(employee);
            response = _responseHandler.Success<string>($"Employee {employee.Name} Deleted Successfully");
            return Ok(response);
        }
        [HttpGet("GetEmployeeByDepartmentId")]
        public async Task<IActionResult> GetEmployeeByDepartmentId(int DepartmentId)
        {
            dynamic response;
            var employees = _employeeRepository.GetEmployeeByDepartmentId(DepartmentId);
            if (employees == null)
            {
                response = _responseHandler.NotFound<string>("No Employees Found");
                return NotFound(response);
            }
            List<EmployeeQueryDTO> mappedEmployees = _mapper.Map<List<Employee>, List<EmployeeQueryDTO>>(employees);
            mappedEmployees = mappedEmployees.Where(emp => emp.IsDeleted == false).ToList();
            response = _responseHandler.Success<List<EmployeeQueryDTO>>(mappedEmployees);
            return Ok(response);
        }
        [HttpGet("GetEmployeeByName")]
        public async Task<IActionResult> GetEmployeeByName(string Name)
        {
            dynamic response;
            var employees = _employeeRepository.GetEmployeeByName(Name);
            if (employees == null)
            {
                response = _responseHandler.NotFound<string>("No Employees Found");
                return NotFound(response);
            }
            List<EmployeeQueryDTO> mappedEmployees = _mapper.Map<List<Employee>, List<EmployeeQueryDTO>>(employees);
            mappedEmployees = mappedEmployees.Where(emp => emp.IsDeleted == false).ToList();
            response = _responseHandler.Success<List<EmployeeQueryDTO>>(mappedEmployees);
            return Ok(response);
        }
        #endregion
    }
}
