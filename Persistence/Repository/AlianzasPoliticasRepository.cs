

using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class AlianzasPoliticasRepository : GeneryRepository<Alianzas_Politicas>, IAlianzasPoliticasRepository
    {
        private readonly SADVOContext _context;
        public AlianzasPoliticasRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public Task<bool> ExisteAlianzaAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Alianzas_Politicas>> GetAlianzasByPartidoAsync(int partidoId)
        {
            throw new NotImplementedException();
        }
    }
}
