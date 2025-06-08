

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class PartidoPoliticoService : GeneryService<Partido_Politico>, IPartidoPoliticoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public PartidoPoliticoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
             _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<bool> ActivarDesactivarPartidoAsync(int partidoId, bool estado)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ActualizarLogoPartidoAsync(int partidoId, string nuevoLogo)
        {
            throw new NotImplementedException();
        }

        public Task<Partido_Politico?> GetPartidoConDetallesAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarSiglasUnicasAsync(string siglas)
        {
            throw new NotImplementedException();
        }
    }
}
