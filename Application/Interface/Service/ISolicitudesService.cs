

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface ISolicitudesService : IGeneryService<Solicitudes>
    {
        Task<IEnumerable<Solicitudes>> GetSolicitudesPendientesAsync();
        Task<bool> ProcesarSolicitudAsync(int solicitudId);
        Task<bool> RechazarSolicitudAsync(int solicitudId);
        Task<IEnumerable<Solicitudes>> GetHistorialSolicitudesAsync();

    }
}
