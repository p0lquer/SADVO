

using SADVO.Application.DTOs.AsignacionDirigente;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Interface.Service
{
    public interface IAsignarCandidatoService : IGeneryService<Asignar_Candidato>
    {
        Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByCandidatoAsync(int candidatoId);
        Task<bool> AsignarCandidatoAPuestoAsync(int candidatoId, int puestoElectivoId, TypeCandidate tipoCandidato);
        Task<bool> ValidarAsignacionDuplicadaAsync(int candidatoId, int puestoElectivoId);
        Task<bool> RemoverAsignacionAsync(int candidatoId, int puestoElectivoId);
        Task CreateAsync(AsignacionCandidatoDto asignacion);
    }
}
