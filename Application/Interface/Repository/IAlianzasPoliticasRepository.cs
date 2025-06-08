

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface IAlianzasPoliticasRepository : IGeneryRepository<Alianzas_Politicas>
    {
        Task<IEnumerable<Alianzas_Politicas>> GetAlianzasByPartidoAsync(int partidoId);

        Task<bool> ExisteAlianzaAsync(int partidoId);

    }
}
