using HRMangmentSystem.BusinessLayer.IRepository;
using HRMangmentSystem.BusinessLayer.Repository;
using HRMangmentSystem.DataAccessLayer.CustomValidators;
using Microsoft.Extensions.DependencyInjection;

namespace HRMangmentSystem.BusinessLayer
{
    public static class BusinessLayerModule
    {
        public static IServiceCollection BusinessLayerModuleDependendcies(this IServiceCollection services)
        {
            services.AddTransient(typeof(IGenericRepositoryAsync<>), typeof(GenericRepositoryAsync<>));
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IGroupRepository, GroupRepository>();
            services.AddTransient<IEmployeeRepository, EmployeeRepository>();
            services.AddTransient<IDepartmentRepository, DepartmentRepository>();
            services.AddTransient<ISettingsRepository, SettingsRepository>();
            services.AddTransient<IAnnualHolidaysRepository, AnnualHolidaysRepository>();
            services.AddTransient<IAttendanceReportRepository, AttendanceReportRepository>();
            services.AddTransient<ISalaryRepository, SalaryRepository>();
            return services;
        }

    }
}
