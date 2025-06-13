

using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Domain.Interface.Repository
{
    public interface IEleccionRepository : IGeneryRepository<Eleccion>
    {
        Task<IEnumerable<Eleccion>> GetEleccionesByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);

        Task<IEnumerable<Eleccion>> GetEleccionesByPartidoAsync(int partidoPoliticoId);

        Task<IEnumerable<Eleccion>> GetEleccionesByPuestoElectivoAsync(int puestoElectivoId);

        Task<IEnumerable<Eleccion>> GetEleccionesByTipoCandidatoAsync(TypeCandidate tipoCandidato);

        Task<IEnumerable<Eleccion>> GetEleccionesRecientesAsync(int cantidad);

    }
}
