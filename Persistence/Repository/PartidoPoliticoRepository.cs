

using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class PartidoPoliticoRepository : GeneryRepository<Partido_Politico>, IPartidoPoliticoRepository
    {
        private readonly SADVOContext _context;
        public PartidoPoliticoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteSiglasAsync(string siglas)
        {
            throw new NotImplementedException();
        }

        public Task<Partido_Politico?> GetBySiglasAsync(string siglas)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Partido_Politico?> GetPartidoWithDirigentesAsync(int partidoId)
        {
            throw new NotImplementedException();
        }
    }
}
