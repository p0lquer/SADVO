

using SADVO.Domain.Entities;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Domain.Interface.Repository
{
    public interface IPartidoPoliticoRepository : IGeneryRepository<Partido_Politico>
    {
        Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync();

        Task<Partido_Politico> GetBySiglasAsync(string siglas);

        Task<List<Partido_Politico?>> GetPartidoWithDirigentesAsync(int partidoId);

        Task<bool> ExisteSiglasAsync(string siglas);


    }
}
