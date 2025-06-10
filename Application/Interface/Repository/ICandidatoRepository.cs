

using System.Numerics;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Interface.Repository
{
    public interface ICandidatoRepository : IGeneryRepository<Candidato>
    {
        Task<IEnumerable<Candidato>> GetCandidatosByPartidoAsync(int partidoId);

        Task<IEnumerable<Candidato>> GetCandidatosActivosAsync();

        Task<IEnumerable<Candidato>> GetCandidatosByPuesto(int puestoElectivoId);

        Task<IEnumerable<Candidato>> GetCandidatosByTypeAsync(TypeCandidate tipoCandidato);

        Task<Candidato?> GetCandidatoWithAsignacionesAsync(int candidatoId);

        Task<bool> ExisteCandidatoByApellidoAsync(string apellido);

    }
}
