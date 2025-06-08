
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class DirigentePoliticoService : GeneryService<Dirigente_Politico>, IDirigentePoliticoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public DirigentePoliticoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<bool> AsignarDirigenteAPartidoAsync(int usuarioId, int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Dirigente_Politico>> GetDirigentesByPartidoAsync(int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoverDirigenteDePartidoAsync(int usuarioId, int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarDirigenteExistenteAsync(int usuarioId, int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }
    }
}
