

using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Domain.Interface.Repository
{
    public interface IAsignarCandidatoRepository : IGeneryRepository<Asignar_Candidato>
    {
        Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByCandidatoAsync(int candidatoId);

        Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByPuestoElectivoAsync(int puestoElectivoId);

      Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByTipoCandidatoAsync(TypeCandidate tipoCandidato);

        Task<bool> ExisteAsignacionAsync(int candidatoId, int puestoElectivoId);

    }
}
