

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class CiudadanoService : GeneryService<Ciudadano>, ICiudadanoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public CiudadanoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<bool> ActivarDesactivarCiudadanoAsync(int ciudadanoId, bool estado)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Ciudadano>> BuscarCiudadanosAsync(string criterio)
        {
            throw new NotImplementedException();
        }

        public Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarCiudadanoUnicoAsync(string numeroIdentificacion, string email)
        {
            throw new NotImplementedException();
        }
    }
}
