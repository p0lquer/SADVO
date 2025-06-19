

namespace SADVO.Application.Interface.Service
{
    public interface IGeneryService<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        IQueryable<T> GetAllQuery();

    }
}
