using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.DataAccessLayer.Context;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.Repository
{
    internal class DepartmentRepository : GenericRepositoryAsync<Department>, IDepartmentRepository
    {
        private readonly DbSet<Department> _departments;
        public DepartmentRepository(HRMangmentCotext dbContext) : base(dbContext)
        {
            _departments = dbContext.Set<Department>();
        }
    }
}
