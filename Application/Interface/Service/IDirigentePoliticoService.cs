

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface IDirigentePoliticoService : IGeneryService<Dirigente_Politico>
    {
        Task<bool> AsignarDirigenteAPartidoAsync(int usuarioId, int partidoPoliticoId);
        Task<IEnumerable<Dirigente_Politico>> GetDirigentesByPartidoAsync(int partidoPoliticoId);
        Task<bool> RemoverDirigenteDePartidoAsync(int usuarioId, int partidoPoliticoId);
        Task<bool> ValidarDirigenteExistenteAsync(int usuarioId, int partidoPoliticoId);

    }
}
