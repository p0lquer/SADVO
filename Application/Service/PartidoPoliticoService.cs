using SADVO.Application.DTOs.PartidoPolitico;
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Service
{
    public class PartidoPoliticoService : GeneryService<Partido_Politico>, IPartidoPoliticoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public PartidoPoliticoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
             _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public async Task<bool> ActivarDesactivarPartidoAsync(int partidoId, bool estado)
        {
            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido político debe ser mayor que cero.", nameof(partidoId));
                }
                var partido = await _alianzasPoliticasRepository.GetByIdAsync(partidoId);
                if (partido == null)
                {
                    throw new KeyNotFoundException($"Partido político con ID {partidoId} no encontrado.");
                }
                partido.Estado = EstadoAlianza.Pendiente;
                await _alianzasPoliticasRepository.UpdateAsync(partido);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al activar/desactivar el partido político con ID {partidoId}", ex);
            }
        }

        public async Task<bool> ActualizarLogoPartidoAsync(int partidoId, string nuevoLogo)
        {
            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido político debe ser mayor que cero.", nameof(partidoId));
                }
                var partido = await _alianzasPoliticasRepository.GetByIdAsync(partidoId);
                if (partido == null)
                {
                    throw new KeyNotFoundException($"Partido político con ID {partidoId} no encontrado.");
                }
               
                
                await _alianzasPoliticasRepository.UpdateAsync(partido);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar el logo del partido político con ID {partidoId}", ex);

            }
        }

        public Task<Partido_Politico?> GetPartidoConDetallesAsync(int partidoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarSiglasUnicasAsync(string siglas)
        {
            throw new NotImplementedException();
        }
    }
}
