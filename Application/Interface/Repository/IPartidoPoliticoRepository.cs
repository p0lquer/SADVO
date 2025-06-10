

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface IPartidoPoliticoRepository : IGeneryRepository<Partido_Politico>
    {
        Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync();

        Task<Partido_Politico?> GetBySiglasAsync(string siglas);

        Task<Partido_Politico?> GetPartidoWithDirigentesAsync(int partidoId);

        Task<bool> ExisteSiglasAsync(string siglas);

    }
}
