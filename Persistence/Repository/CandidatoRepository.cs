using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class CandidatoRepository : GeneryRepository<Candidato>, ICandidatoRepository
    {
        private readonly SADVOContext _context;
        public CandidatoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteCandidatoByApellidoAsync(string apellido)
        {
            if(string.IsNullOrWhiteSpace(apellido) || apellido.Length > 12)
            {
                throw new ArgumentException("El apellido no puede ser null o vacío.", nameof(apellido));
            }
           
                try
                {
                    return Task.FromResult(_context.Candidatos.Any(c => c.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase)));
                }
                catch (Exception ex)
                {
                    
                    throw new Exception("Error checking if candidate exists by last name", ex);
                }
        }
        public Task<IEnumerable<Candidato>> GetCandidatosActivosAsync()
        {

            try
            {
                return Task.FromResult(_context.Candidatos.Where(c => c.EsActivo == true).AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active candidates", ex);
            }
        }


        public Task<IEnumerable<Candidato>> GetCandidatosByPartidoAsync(int partidoId)
        {
            if (partidoId > 0)
            {

                try
                {
                    return Task.FromResult(_context.Candidatos.Where(c => c.PartidoId == partidoId).AsEnumerable());
                }
                catch (Exception ex)
                {
                    throw  new Exception($"Error retrieving candidates for party with ID {partidoId}", ex);
                }
            }
            else
            {
                throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
            }
        }
        public Task<IEnumerable<Candidato>> GetCandidatosByPuesto(int puestoElectivoId)
        {
            if (puestoElectivoId > 0)
            {
                try
                {
                    return Task.FromResult(_context.Candidatos.Where(c => c.PuestoElectivoId == puestoElectivoId).AsEnumerable());
                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving candidates for electoral position with ID {puestoElectivoId}", ex);
                }
            }
            else
            {
                throw new ArgumentException("El ID del puesto electivo debe ser mayor que cero.", nameof(puestoElectivoId));
            }
        }

        public async Task<Candidato?> GetCandidatoWithAsignacionesAsync(int candidatoId)
        {
            try
            {
                if (candidatoId <= 0)
                {
                    throw new ArgumentException("El ID del candidato debe ser mayor que cero.", nameof(candidatoId));
                }
                return await _context.Candidatos
                    .Include(c => c.Asignar_Candidato)
                    .FirstOrDefaultAsync(c => c.Id == candidatoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving candidate with ID {candidatoId} and their assignments", ex);

            }
        }

        public Task<IEnumerable<Candidato>> GetCandidatosByTypeAsync(TypeCandidate tipoCandidato)
        {
            try
            {
                return Task.FromResult(_context.Candidatos.Where(c => c.TypeCandidate == tipoCandidato).AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving candidates of type {tipoCandidato}", ex);
            }
        }
    }
}
