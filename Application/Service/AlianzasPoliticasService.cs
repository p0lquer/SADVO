
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class AlianzasPoliticasService : GeneryService<Alianzas_Politicas>, IAlianzasPoliticasServicen
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;

        public AlianzasPoliticasService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base( alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<bool> CrearAlianzaAsync(Alianzas_Politicas alianza)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Alianzas_Politicas>> GetAlianzasByPartidoAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarAlianzaExistenteAsync(int partidoId)
        {
            throw new NotImplementedException();
        }
        
    }
    
}
