using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class EleccionService : GeneryService<Eleccion>, IEleccionService
    {
        private readonly IEleccionRepository _eleccionRepository; // CORREGIDO: Usar IEleccionRepository

        public EleccionService(IEleccionRepository eleccionRepository) // CORREGIDO: Inyectar IEleccionRepository
            : base(eleccionRepository)
        {
            _eleccionRepository = eleccionRepository ?? throw new ArgumentNullException(nameof(eleccionRepository));
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

            // CORREGIDO: Usar el repositorio correcto
            return await _eleccionRepository.GetEleccionesByFechaRangoAsync(fechaInicio, fechaFin);
        }

        public async Task<IEnumerable<Eleccion>> GetHistorialEleccionesAsync(int partidoId)
        {
            if (partidoId <= 0)
            {
                throw new ArgumentException("El ID del partido político debe ser mayor que cero.", nameof(partidoId));
            }

            // CORREGIDO: Usar el repositorio correcto
            return await _eleccionRepository.GetEleccionesByPartidoAsync(partidoId);
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

                // CORREGIDO: Usar directamente el repositorio base
                var resultado = await _eleccionRepository.AddAsync(eleccion);
                return resultado != null;
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

                // CORREGIDO: Usar el repositorio correcto
                var elecciones = await _eleccionRepository.GetEleccionesByFechaRangoAsync(fecha, fecha);
                return elecciones.Any();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar la fecha de la elección.", ex);
            }
        }

        // MÉTODOS ADICIONALES que deberías agregar a la interfaz IEleccionService
        public async Task<IEnumerable<Eleccion>> GetEleccionesByPuestoElectivoAsync(int puestoElectivoId)
        {
            if (puestoElectivoId <= 0)
            {
                throw new ArgumentException("El ID del puesto electivo debe ser mayor que cero.", nameof(puestoElectivoId));
            }

            return await _eleccionRepository.GetEleccionesByPuestoElectivoAsync(puestoElectivoId);
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesByTipoCandidatoAsync(Domain.Enumns.TypeCandidate tipoCandidato)
        {
            if (!Enum.IsDefined(typeof(Domain.Enumns.TypeCandidate), tipoCandidato))
            {
                throw new ArgumentException("El tipo de candidato no es válido.", nameof(tipoCandidato));
            }

            return await _eleccionRepository.GetEleccionesByTipoCandidatoAsync(tipoCandidato);
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesRecientesAsync(int cantidad)
        {
            if (cantidad <= 0)
            {
                throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));
            }

            return await _eleccionRepository.GetEleccionesRecientesAsync(cantidad);
        }
    }
}