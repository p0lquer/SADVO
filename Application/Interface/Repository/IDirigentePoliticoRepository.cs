

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface IDirigentePoliticoRepository : IGeneryRepository<Dirigente_Politico>
    {
        Task<IEnumerable<Dirigente_Politico>> GetDirigentesByPartidoAsync(int partidoPoliticoId);

        Task<IEnumerable<Dirigente_Politico>> GetDirigentesByUsuarioAsync(int usuarioId);

        Task<Dirigente_Politico?> GetDirigenteWithDetailsAsync(int dirigenteId);

        Task<bool> ExisteDirigenteAsync(int usuarioId, int partidoPoliticoId);

    }
}
