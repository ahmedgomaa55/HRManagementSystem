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
    public class GroupRepository : GenericRepositoryAsync<Group>, IGroupRepository
    {
        private readonly DbSet<Group> _group;
        public GroupRepository(HRMangmentCotext dbContext) : base(dbContext)
        {
            _group = dbContext.Set<Group>();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _group.Include(group => group.Permissions).FirstOrDefaultAsync(group => group.Id == id);
        }
    }
}
