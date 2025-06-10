

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class EleccionService : GeneryService<Eleccion>, IEleccionService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public EleccionService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<IEnumerable<Eleccion>> GetEleccionesEnRangoFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Eleccion>> GetHistorialEleccionesAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ProgramarEleccionAsync(Eleccion eleccion)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarFechaEleccionAsync(DateTime fecha)
        {
            throw new NotImplementedException();
        }
    }
}
