using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class CandidatoRepository : GeneryRepository<Candidato>, ICandidatoRepository
    {
        private readonly SADVOContext _context;
        public CandidatoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteCandidatoByApellidoAsync(string apellido)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candidato>> GetCandidatosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candidato>> GetCandidatosByPartidoAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candidato>> GetCandidatosByTipoAsync(TypeCandidate tipoCandidato)
        {
            throw new NotImplementedException();
        }

        public Task<Candidato?> GetCandidatoWithAsignacionesAsync(int candidatoId)
        {
            throw new NotImplementedException();
        }
    }
}
