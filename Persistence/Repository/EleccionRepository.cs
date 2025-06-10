

using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class EleccionRepository : GeneryRepository<Eleccion>,
        IEleccionRepository
    {
        private readonly SADVOContext _context;
        public EleccionRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<IEnumerable<Eleccion>> GetEleccionesByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Eleccion>> GetEleccionesByPartidoAsync(int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Eleccion>> GetEleccionesByPuestoElectivoAsync(int puestoElectivoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Eleccion>> GetEleccionesByTipoCandidatoAsync(TypeCandidate tipoCandidato)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Eleccion>> GetEleccionesRecientesAsync(int cantidad)
        {
            throw new NotImplementedException();
        }
    }
}
