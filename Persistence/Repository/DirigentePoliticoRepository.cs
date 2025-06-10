

using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class DirigentePoliticoRepository : GeneryRepository<Dirigente_Politico>, IDirigentePoliticoRepository
    {
        private readonly SADVOContext _context;
        public DirigentePoliticoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteDirigenteAsync(int usuarioId, int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dirigente_Politico>> GetDirigentesByPartidoAsync(int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dirigente_Politico>> GetDirigentesByUsuarioAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<Dirigente_Politico?> GetDirigenteWithDetailsAsync(int dirigenteId)
        {
            throw new NotImplementedException();
        }
    }
}
