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

        public async Task<IEnumerable<Eleccion>> GetEleccionesEnRangoFechaAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio == default || fechaFin == default)
            {
                throw new ArgumentException("Las fechas de inicio y fin no pueden ser nulas.");
            }
            if (fechaInicio > fechaFin)
            {
                throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");
            }
            return await _alianzasPoliticasRepository.GetEleccionesByFechaRangoAsync(fechaInicio, fechaFin);

        }

        public async Task<IEnumerable<Eleccion>> GetHistorialEleccionesAsync(int partidoId)
        {
            if (partidoId <= 0)
            {
                throw new ArgumentException("El ID del partido político debe ser mayor que cero.", nameof(partidoId));
            }
            return await _alianzasPoliticasRepository.GetEleccionesByPartidoAsync(partidoId);

        }

        public async Task<bool> ProgramarEleccionAsync(Eleccion eleccion)
        {
            try
            {
                if (eleccion == null)
                {
                    throw new ArgumentNullException(nameof(eleccion), "La elección no puede ser nula.");
                }
                if (eleccion.FechaOcurrida == default)
                {
                    throw new ArgumentException("La fecha de la elección no puede ser nula.", nameof(eleccion.FechaOcurrida));
                }
                return await _alianzasPoliticasRepository.AddAsync(eleccion) != null;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al programar la elección.", ex);

            }
        }

        public async Task<bool> ValidarFechaEleccionAsync(DateTime fecha)
        {
            try
            {
                if (fecha == default)
                {
                    throw new ArgumentException("La fecha de la elección no puede ser nula.", nameof(fecha));
                }
                var elecciones = await _alianzasPoliticasRepository.GetEleccionesByFechaRangoAsync(fecha, fecha);
                return elecciones.Any();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar la fecha de la elección.", ex);

            }
        }
    }
}
