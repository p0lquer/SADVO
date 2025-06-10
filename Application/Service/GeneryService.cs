using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
namespace SADVO.Application.Service
{
    public class GeneryService<T> : IGeneryService<T> where T : class
    {
        private IAlianzasPoliticasRepository alianzasPoliticasRepository;

        public GeneryService(IAlianzasPoliticasRepository alianzasPoliticasRepository)
        {
            this.alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<T> CreateAsync(T entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<T>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<T> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<T> UpdateAsync(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
