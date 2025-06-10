
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class AlianzasPoliticasService : GeneryService<Alianzas_Politica>, IAlianzasPoliticasServicen
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;

        public AlianzasPoliticasService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base( alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<bool> CrearAlianzaAsync(Alianzas_Politica alianza)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarAlianzaExistenteAsync(int partidoId)
        {
            throw new NotImplementedException();
        }
        
    }
    
}
