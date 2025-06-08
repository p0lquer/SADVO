

using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class SolicitudesRepository : GeneryRepository<Solicitudes>, ISolicitudesRepository
    {
        private readonly SADVOContext _context;
        public SolicitudesRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<IEnumerable<Solicitudes>> GetSolicitudesByFechaAsync(DateTime fecha)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Solicitudes>> GetSolicitudesPendientesAsync()
        {
            throw new NotImplementedException();
        }
    }
    
}
