using HRMangmentSystem.DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HRMangmentSystem.BusinessLayer.IRepository
{
    public interface IGroupRepository : IGenericRepositoryAsync<Group>
    {
        public Task<Group> GetGroupById(int id);

    }
}
