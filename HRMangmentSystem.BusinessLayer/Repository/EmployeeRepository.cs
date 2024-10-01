using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.Repository
{
    public class EmployeeRepository : GenericRepositoryAsync<Employee>, IEmployeeRepository
    {
        private readonly DbSet<Employee> _employees;

        public EmployeeRepository(HRMangmentCotext dbContext) : base(dbContext)
        {
            _employees = dbContext.Set<Employee>();
        }
        public override List<Employee> GetTableAsTracking()
        {
            return _employees.Include(employee => employee.Department).ToList();
        }
        public async Task<Employee> GetEmployeeByNationalId(string nationalId)
        {
            return await _employees.Include(employee => employee.Department).FirstOrDefaultAsync(emp => emp.NationalId == nationalId);
        }
        public List<Employee> GetEmployeeByDepartmentId(int departmentId)
        {
            return _employees.Include(employee => employee.Department).Where(emp => emp.DepartmentId == departmentId).ToList();
        }
        public List<Employee> GetEmployeeByName(string name)
        {
            return _employees.Include(dept => dept.Department).Select(employee => employee).Where(emp => emp.Name.Contains(name)).ToList();
        }
    }
}
