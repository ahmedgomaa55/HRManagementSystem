using HRManagementSystem.DataAccessLayer.Models;
using HRMangmentSystem.DataAccessLayer.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.DataAccessLayer.Context
{
    public class HRMangmentCotext : IdentityDbContext<ApplicationUser>
    {
        public HRMangmentCotext()
        {
        }
        public HRMangmentCotext(DbContextOptions<HRMangmentCotext> options) : base(options)
        {
        }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<GeneralSettings> GeneralSettings { get; set; }
        public DbSet<AnnualHolidays> AnnualHolidays { get; set;}
        public DbSet<AttendanceRecord> AttendanceRecords { get; set; }

    }

}
