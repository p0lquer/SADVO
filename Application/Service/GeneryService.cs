using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
namespace SADVO.Application.Service
{
    public class GeneryService<T> : IGeneryService<T> where T : class
    {
        private IGeneryRepository<T> generyRepository;

        public GeneryService(IGeneryRepository<T> generyRepository)
        {
            this.generyRepository = generyRepository;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            await generyRepository.AddAsync(entity);
            return entity;
        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID must be greater than zero.", nameof(id));
                return await generyRepository.DeleteAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error deleting entity with ID {id}", ex);
            }
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await generyRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all entities", ex);
            }
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            try
            {
                var entity = await generyRepository.GetByIdAsync(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"No se encontró la entidad con ID {id}.");
                }
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving entity with ID {id}", ex);
            }
        }

        public virtual async Task<T> UpdateAsync(T entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

                return await generyRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating entity", ex);
            }
        }
    }
}