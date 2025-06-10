

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface IAlianzasPoliticasRepository : IGeneryRepository<Alianzas_Politica>
    {
        Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId);

        Task<bool> ExisteAlianzaAsync(int partidoId);

    }
}
