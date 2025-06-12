

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface IGeneryRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id);
        Task<T> AddAsync(T entity);
        Task<T> UpdateAsync(T entity);
        Task<bool> DeleteAsync(int id);
        Task<Partido_Politico?> GetBySiglasAsync(string siglas);
    }
}
