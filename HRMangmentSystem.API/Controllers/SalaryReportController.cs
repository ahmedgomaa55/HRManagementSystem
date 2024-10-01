using AutoMapper;
using HRMangmentSystem.API.DTOS.SalaryReportDTO;
using HRMangmentSystem.API.ResponseBase;
using HRMangmentSystem.BusinessLayer.Helpers;
using HRMangmentSystem.BusinessLayer.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HRMangmentSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "SuperAdmin, Admin")]

    public class SalaryReportController : ControllerBase
    {
        private readonly ISalaryRepository _salaryRepository;
        private readonly IMapper _mapper;
        private readonly ResponseHandler _responseHandler;
        public SalaryReportController(ISalaryRepository salaryRepository, IMapper mapper, ResponseHandler responseHandler)
        {
            _salaryRepository = salaryRepository;
            _mapper = mapper;
            _responseHandler = responseHandler;
        }
        [HttpGet("GetSalaries")]
        public IActionResult GetSalaries(string? employeeName, string date)
        {
            dynamic response;
            if (ModelState.IsValid)
            {
                var salaryReport = _salaryRepository.CalculateSalary(employeeName, DateOnly.Parse(date));
                if (salaryReport == null || salaryReport.Count == 0)
                {
                    response = _responseHandler.NotFound<string>("No Salaries Found");
                    return NotFound(response);
                }
                var mappedSalaries = _mapper.Map<List<SalaryReportData>, List<SalaryReportQueryDTO>>(salaryReport);
                response = _responseHandler.Success(mappedSalaries);
                return Ok(response);
            }
            response = _responseHandler.BadRequest<string>("Invalid Data");
            return BadRequest(response);
        }
    }
}
