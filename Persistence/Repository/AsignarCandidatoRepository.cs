
using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class AsignarCandidatoRepository : GeneryRepository<Asignar_Candidato>, IAsignarCandidatoRepository
    {
        private readonly SADVOContext _context;
        public AsignarCandidatoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteAsignacionAsync(int candidatoId, int puestoElectivoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByCandidatoAsync(int candidatoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByPuestoElectivoAsync(int puestoElectivoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByTipoCandidatoAsync(TypeCandidate tipoCandidato)
        {
            throw new NotImplementedException();
        }
    }
    
}
