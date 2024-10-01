using Microsoft.EntityFrameworkCore.Storage;

namespace HRMangmentSystem.BusinessLayer.IRepository
{
    public interface IGenericRepositoryAsync<T> where T : class
    {
        Task<T> GetByIdAsync(int id);
        Task SaveChangesAsync();
        List<T> GetTableAsTracking();
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);
    }
}
