using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
namespace SADVO.Application.Service
{
    public class GeneryService<T> : IGeneryService<T> where T : class
    {
        private IAlianzasPoliticasRepository alianzasPoliticasRepository;

        public GeneryService(IAlianzasPoliticasRepository alianzasPoliticasReposit)
        {
           alianzasPoliticasRepository = alianzasPoliticasReposit;
        }

        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

             await alianzasPoliticasRepository.AddAsync(entity);
            return entity;
        }

        public virtual async  Task<bool> DeleteAsync(int id)
        {
            try
            {
                if (id <= 0)
                    throw new ArgumentException("ID must be greater than zero.", nameof(id));
                return await alianzasPoliticasRepository.DeleteAsync(id);
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
                return (IEnumerable<T>)await alianzasPoliticasRepository.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all entities", ex);
            }
        }

        public virtual async  Task<T> GetByIdAsync(int id)
        {
            try
            {
                var entity = await alianzasPoliticasRepository.GetByIdAsync(id);
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

                return await alianzasPoliticasRepository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating entity", ex);

            }
        }
    }
}
