

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface ISolicitudesRepository : IGeneryRepository<Solicitudes>
    {
        Task<IEnumerable<Solicitudes>> GetSolicitudesPendientesAsync();

        Task<IEnumerable<Solicitudes>> GetSolicitudesByFechaAsync(DateTime fecha);


    }
}
