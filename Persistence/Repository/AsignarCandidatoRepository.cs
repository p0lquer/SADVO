
using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class AsignarCandidatoRepository : GeneryRepository<Asignar_Candidato>, IAsignarCandidatoRepository
    {
        private readonly SADVOContext _context;
        public AsignarCandidatoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExisteAsignacionAsync(int candidatoId, int puestoElectivoId)
        {
            try
            {
                if (candidatoId <= 0 || puestoElectivoId <= 0)
                {
                    throw new ArgumentException("Los IDs de candidato y puesto electivo deben ser mayores que cero.");
                }
                return await Task.FromResult(_context.AsignarCandidatos.Any(a => a.CandidatoId == candidatoId && a.PuestoElectivoId == puestoElectivoId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if assignment exists for candidate ID {candidatoId} and position ID {puestoElectivoId}", ex);

            }
        }

        public async Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByCandidatoAsync(int candidatoId)
        {
            try
            {
                if (candidatoId <= 0)
                {
                    throw new ArgumentException("El ID del candidato debe ser mayor que cero.", nameof(candidatoId));
                }
                return await Task.FromResult(_context.AsignarCandidatos
                    .Where(a => a.CandidatoId == candidatoId)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assignments for candidate with ID {candidatoId}", ex);

            }
        }

        public async Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByPuestoElectivoAsync(int puestoElectivoId)
        {
            try
            {
                if (puestoElectivoId <= 0)
                {
                    throw new ArgumentException("El ID del puesto electivo debe ser mayor que cero.", nameof(puestoElectivoId));
                }
                return await Task.FromResult(_context.AsignarCandidatos
                    .Where(a => a.PuestoElectivoId == puestoElectivoId)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assignments for position with ID {puestoElectivoId}", ex);
            }
        }

        public async Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByTipoCandidatoAsync(TypeCandidate tipoCandidato)
        {
            try
            {
                if (!Enum.IsDefined(typeof(TypeCandidate), tipoCandidato))
                {
                    throw new ArgumentException("Tipo de candidato no válido.", nameof(tipoCandidato));
                }
                return await Task.FromResult(_context.AsignarCandidatos
                    .Where(a => a.Tipo_Candidato == tipoCandidato)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assignments for candidate type {tipoCandidato}", ex);

            }
        }
    }
    
}
