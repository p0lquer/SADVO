

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface ICandidatoService : IGeneryService<Candidato>
    {
        Task<IEnumerable<Candidato>> GetCandidatosByPartidoAsync(int partidoId);
        Task<IEnumerable<Candidato>> GetCandidatosActivosAsync();
        Task<bool> ActivarDesactivarCandidatoAsync(int candidatoId, bool estado);
        Task<bool> ActualizarFotoCandidatoAsync(int candidatoId, string nuevaFoto);
        Task<bool> ValidarCandidatoUnicoAsync(string apellido, int partidoId);
    }
}
