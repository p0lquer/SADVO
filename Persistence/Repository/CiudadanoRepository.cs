

using SADVO.Application.Interface.Repository;
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
        public Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion)
        {
            throw new NotImplementedException();
        }
        public Task<Ciudadano?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
        public Task<IEnumerable<Ciudadano>> GetCiudadanosActivosAsync()
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExisteNumeroIdentificacionAsync(string numeroIdentificacion)
        {
            throw new NotImplementedException();
        }
        public Task<bool> ExisteEmailAsync(string email)
        {
            throw new NotImplementedException();
        }
    }
    
}
