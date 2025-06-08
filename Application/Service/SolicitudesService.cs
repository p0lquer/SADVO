

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class SolicitudesService : GeneryService<Solicitudes>, ISolicitudesService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public SolicitudesService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository ?? throw new ArgumentNullException(nameof(alianzasPoliticasRepository));
        }

        public Task<IEnumerable<Solicitudes>> GetHistorialSolicitudesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Solicitudes>> GetSolicitudesPendientesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProcesarSolicitudAsync(int solicitudId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RechazarSolicitudAsync(int solicitudId)
        {
            throw new NotImplementedException();
        }
    }
}
