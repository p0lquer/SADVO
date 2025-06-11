

using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class AlianzasPoliticasRepository : GeneryRepository<Alianzas_Politica>, IAlianzasPoliticasRepository
    {
        private readonly SADVOContext _context;
        public AlianzasPoliticasRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public Task<bool> ExisteAlianzaAsync(int partidoId)
        {

            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
                }
                return Task.FromResult(_context.AlianzasPoliticas.Any(a => a.PartidoSolicitanteId == partidoId || a.PartidoReceptorId == partidoId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if alliance exists for party with ID {partidoId}", ex);
            }
        }
        public Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId)
        {
            if (partidoId > 0)
            {
                try
                {
                    return Task.FromResult(_context.AlianzasPoliticas
                        .Where(a => a.PartidoSolicitanteId == partidoId || a.PartidoReceptorId == partidoId)
                        .AsEnumerable());

                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving alliances for party with ID {partidoId}", ex);
                }
            }
            else
            {
                throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
            }
        }
    }
}
