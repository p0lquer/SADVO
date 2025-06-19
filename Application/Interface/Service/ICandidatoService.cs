

using SADVO.Application.DTOs.Candidato;
using SADVO.Application.ViewModels.CandidatoVM;
using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface ICandidatoService : IGeneryService<Candidato>
    {
        Task<IEnumerable<CandidatoDto>> GetCandidatosByPartidoAsync(int partidoId);
        Task<IEnumerable<CandidatoDto>> GetCandidatosActivosAsync();
        Task<bool> ActivarDesactivarCandidatoAsync(int candidatoId, bool estado);
        Task<bool> ActualizarFotoCandidatoAsync(int candidatoId, string nuevaFoto);
          Task<bool> ValidarCandidatoUnicoAsync(string apellido);

    

       
    }
}
