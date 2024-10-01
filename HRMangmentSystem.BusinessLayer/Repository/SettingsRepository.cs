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
    public class SettingsRepository : GenericRepositoryAsync<GeneralSettings>, ISettingsRepository
    {
        private readonly DbSet<GeneralSettings> _settings;
        public SettingsRepository(HRMangmentCotext dbContext) : base(dbContext)
        {
            _settings = dbContext.Set<GeneralSettings>();
        }


    }
}
