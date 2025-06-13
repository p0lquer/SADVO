    

using SADVO.Domain.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class CiudadanoRepository : GeneryRepository<Ciudadano>, ICiudadanoRepository
    {
        private readonly SADVOContext _context;
        public CiudadanoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        public async Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroIdentificacion) || numeroIdentificacion.Length > 12)
                {
                    throw new ArgumentException("El número de identificación no puede ser null o vacío y debe tener un máximo de 12 caracteres.", nameof(numeroIdentificacion));
                }
                return await Task.FromResult(_context.Ciudadanos.FirstOrDefault(c => c.NumeroIdentificacion.Equals(numeroIdentificacion, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving citizen by identification number {numeroIdentificacion}", ex);

            }
        }
        public async Task<Ciudadano?> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await Task.FromResult(_context.Ciudadanos.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving citizen by email {email}", ex);

            }
        }
        public async Task<IEnumerable<Ciudadano>> GetCiudadanosActivosAsync()
        {
            try
            {
                return await Task.FromResult(_context.Ciudadanos.Where(c => c.EsActivo).AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active citizens", ex);

            }
        }
        public async Task<bool> ExisteNumeroIdentificacionAsync(string numeroIdentificacion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroIdentificacion) || numeroIdentificacion.Length > 12)
                {
                    throw new ArgumentException("El número de identificación no puede ser null o vacío y debe tener un máximo de 12 caracteres.", nameof(numeroIdentificacion));
                }
                return await Task.FromResult(_context.Ciudadanos.Any(c => c.NumeroIdentificacion.Equals(numeroIdentificacion, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if citizen exists by identification number {numeroIdentificacion}", ex);
            }

        }
        public async Task<bool> ExisteEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await Task.FromResult(_context.Ciudadanos.Any(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if citizen exists by email {email}", ex);
            }
        }
    }
    
}
