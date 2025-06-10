

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class CandidatoService : GeneryService<Candidato>, ICandidatoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public CandidatoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<bool> ActivarDesactivarCandidatoAsync(int candidatoId, bool estado)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ActualizarFotoCandidatoAsync(int candidatoId, string nuevaFoto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candidato>> GetCandidatosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Candidato>> GetCandidatosByPartidoAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarCandidatoUnicoAsync(string apellido, int partidoId)
        {
            throw new NotImplementedException();
        }
    }
}
