using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class EleccionRepository : GeneryRepository<Eleccion>,
        IEleccionRepository
    {
        private readonly SADVOContext _context;
        public EleccionRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (fechaInicio == default || fechaFin == default)
                {
                    throw new ArgumentException("Las fechas de inicio y fin no pueden ser nulas.");
                }
                if (fechaInicio > fechaFin)
                {
                    throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");
                }
                return await Task.FromResult(_context.Elecciones
                    .Where(e => e.FechaOcurrida >= fechaInicio && e.FechaOcurrida <= fechaFin)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving elections between {fechaInicio} and {fechaFin}", ex);

            }
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesByPartidoAsync(int partidoPoliticoId)
        {
            try
            {
                if (partidoPoliticoId <= 0)
                {
                    throw new ArgumentException("El ID del partido político debe ser mayor que cero.", nameof(partidoPoliticoId));
                }
                return await Task.FromResult(_context.Elecciones
                    .Where(e => e.Id == partidoPoliticoId)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving elections for party with ID {partidoPoliticoId}", ex);
            }
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesByPuestoElectivoAsync(int puestoElectivoId)
        {
            try
            {
                if (puestoElectivoId <= 0)
                {
                    throw new ArgumentException("El ID del puesto electivo debe ser mayor que cero.", nameof(puestoElectivoId));
                }
                return await Task.FromResult(_context.Elecciones
                    .Where(e => e.Id == puestoElectivoId)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving elections for electoral position with ID {puestoElectivoId}", ex);


            }
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesByTipoCandidatoAsync(TypeCandidate tipoCandidato)
        {
            try
            {
                if (!Enum.IsDefined(typeof(TypeCandidate), tipoCandidato))
                {
                    throw new ArgumentException("El tipo de candidato no es válido.", nameof(tipoCandidato));
                }
                return await Task.FromResult(_context.Elecciones
                    .Where(e => e.TypeCandidate == tipoCandidato)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving elections for candidate type {tipoCandidato}", ex);

            }
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesRecientesAsync(int cantidad)
        {
            try
            {
                if (cantidad <= 0)
                {
                    throw new ArgumentException("La cantidad debe ser mayor que cero.", nameof(cantidad));
                }
                return await Task.FromResult(_context.Elecciones
                    .OrderByDescending(e => e.FechaOcurrida)
                    .Take(cantidad)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving the most recent {cantidad} elections", ex);

            }
        }
    }
}
