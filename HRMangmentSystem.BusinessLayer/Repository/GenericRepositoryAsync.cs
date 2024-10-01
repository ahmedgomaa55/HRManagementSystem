using HRMangmentSystem.BusinessLayer.IRepository;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HRMangmentSystem.DataAccessLayer.Context;

namespace HRMangmentSystem.BusinessLayer.Repository
{
    public class GenericRepositoryAsync<T> : IGenericRepositoryAsync<T> where T : class
    {
        #region Vars / Props

        protected readonly HRMangmentCotext _dbContext;

        #endregion

        #region Constructor(s)
        public GenericRepositoryAsync(HRMangmentCotext dbContext)
        {
            _dbContext = dbContext;
        }
        #endregion
        #region Actions
        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public virtual async Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
        }

        public virtual  List<T> GetTableAsTracking()
        {
            return  _dbContext.Set<T>().AsNoTracking().ToList();
        }

        public async Task SaveChangesAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        public virtual async Task UpdateAsync(T entity)
        {
            _dbContext.Set<T>().Update(entity);
            await _dbContext.SaveChangesAsync();
        }
        #endregion
    }
}
