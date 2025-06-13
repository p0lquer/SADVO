using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Application.Service
{
    public class CandidatoService : GeneryService<Candidato>, ICandidatoService
    {
        private readonly IGeneryRepository<Candidato> _candidatoRepository;
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;

        public CandidatoService(IGeneryRepository<Candidato> candidatoRepository, IAlianzasPoliticasRepository alianzasPoliticasRepository)
            : base(candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public async Task<bool> ActivarDesactivarCandidatoAsync(int candidatoId, bool estado)
        {
            try
            {
                if (candidatoId <= 0)
                {
                    throw new ArgumentException("El ID del candidato debe ser mayor que cero.", nameof(candidatoId));
                }
                var candidato = await _alianzasPoliticasRepository.GetByIdAsync(candidatoId);
                if (candidato == null)
                {
                    throw new KeyNotFoundException($"Candidato con ID {candidatoId} no encontrado.");
                }
                candidato.Estado = EstadoAlianza.Pendiente;
                await _alianzasPoliticasRepository.UpdateAsync(candidato);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al activar/desactivar el candidato con ID {candidatoId}", ex);

            }
        }

        public async Task<bool> ActualizarFotoCandidatoAsync(int candidatoId, string nuevaFoto)
        {
            try
            {
                if (candidatoId <= 0)
                {
                    throw new ArgumentException("El ID del candidato debe ser mayor que cero.", nameof(candidatoId));
                }
                var candidato = await _alianzasPoliticasRepository.GetByIdAsync(candidatoId);
                if (candidato == null)
                {
                    throw new KeyNotFoundException($"Candidato con ID {candidatoId} no encontrado.");
                }
                candidato.Estado  = EstadoAlianza.Pendiente;
                await _alianzasPoliticasRepository.UpdateAsync(candidato);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la foto del candidato con ID {candidatoId}", ex);
            }
        }

        public async Task<IEnumerable<Candidato>> GetCandidatosActivosAsync()
        {
            try
            {
                var candidatos = await _alianzasPoliticasRepository.GetAllAsync();
                if (candidatos == null || !candidatos.Any())
                {
                    return Enumerable.Empty<Candidato>();
                }
                return (IEnumerable<Candidato>)candidatos.Where(c => c.Estado == EstadoAlianza.Pendiente);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active candidates", ex);
            }
        }


        public async Task<IEnumerable<Candidato>> GetCandidatosByPartidoAsync(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
                }
                var candidatos = await _alianzasPoliticasRepository.GetAllAsync();
                return (IEnumerable<Candidato>) candidatos.Where(c => c.PartidoSolicitanteId == partidoId && c.Estado == EstadoAlianza.Pendiente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving candidates for party with ID {partidoId}", ex);

            }
        }

        public async Task<bool> ValidarCandidatoUnicoAsync(string apellido, int partidoId)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apellido) || apellido.Length > 12)
                {
                    throw new ArgumentException("El apellido no puede ser null o vacío y debe tener un máximo de 12 caracteres.", nameof(apellido));
                }
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
                }
                var candidatos = await _alianzasPoliticasRepository.GetAllAsync();
                return !candidatos.Any(c => c.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase) && c.PartidoSolicitanteId == partidoId && c.Estado == EstadoAlianza.Pendiente);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating unique candidate by last name {apellido} and party ID {partidoId}", ex);
            }
        }
    }
}
