using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;
using SADVO.Interfaces.Interface.Repository;

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
                throw new Exception("Error adding entity.", ex);
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity != null)
                {
                    var isActiveProperty = entity.GetType().GetProperty("EsActivo");
                    if (isActiveProperty != null && isActiveProperty.PropertyType == typeof(bool))
                    {
                        isActiveProperty.SetValue(entity, false);
                        _context.Entry(entity).State = EntityState.Modified;
                    }
                    else
                    {
                        Entities.Remove(entity);
                    }
                    await _context.SaveChangesAsync();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception("Error deleting entity.", ex);
            }
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            try
            {
                return await Entities.ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all entities.", ex);
            }
        }

        public async Task<T> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("ID must be greater than zero.", nameof(id));

            try
            {
                var entity = await Entities.FindAsync(id);
                if (entity == null)
                    throw new KeyNotFoundException($"Entity with ID {id} not found.");
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving entity by ID.", ex);
            }
        }

        public async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");

            try
            {
                Entities.Update(entity);
                await _context.SaveChangesAsync();
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating entity.", ex);
            }
        }

        //public async Task<Partido_Politico?> GetBySiglasAsync(string siglas)
        //{
        //    if (string.IsNullOrWhiteSpace(siglas))
        //        throw new ArgumentException("Siglas cannot be null or empty.", nameof(siglas));

        //    try
        //    {
        //        return await _context.PartidosPoliticos
        //            .FirstOrDefaultAsync(p => p.Siglas == siglas);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception("Error retrieving Partido_Politico by siglas.", ex);
        //    }
        //}
    }
}