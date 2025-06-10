using Microsoft.EntityFrameworkCore;
using SADVO.Application.Interface.Repository;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class GeneryRepository<T> : IGeneryRepository<T> where T : class
    {
        private readonly SADVOContext _context;
        public readonly DbSet<T> Entities;
        public GeneryRepository(SADVOContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            Entities = _context.Set<T>();
        }


        public async Task<T> AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            try
            {
                await Entities.AddAsync(entity);
                await _context.SaveChangesAsync();
                return entity;

            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public async Task<bool> DeleteAsync(int id)
        {
            if (id == 0) // Fixed condition to check for empty ID  
                throw new ArgumentException("ID cannot be empty.", nameof(id));

            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity != null)
                {
                    var isActiveProperty = entity.GetType().GetProperty("IsActive");
                    if (isActiveProperty != null && isActiveProperty.PropertyType == typeof(bool))
                    {
                        isActiveProperty.SetValue(entity, false);
                        _context.Entry(entity).State = EntityState.Modified;
                    }
                    else
                    {
                        _context.Remove(entity);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                var entities = await Entities.ToListAsync();
                return entities;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Task<T> GetByIdAsync(int id)
        {
            if (id == 0)
                throw new ArgumentException("ID cannot be empty.", nameof(id));
            try
            {
                var entity = Entities.Find(id);
                if (entity == null)
                {
                    throw new KeyNotFoundException($"Entity with ID {id} not found.");
                }
                return Task.FromResult(entity);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
            try
            {
                Entities.Update(entity);
                _context.SaveChanges();
                return Task.FromResult(entity);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
