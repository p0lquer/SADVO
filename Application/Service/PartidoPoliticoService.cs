using SADVO.Application.DTOs.PartidoPolitico;
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Service
{
    namespace SADVO.Application.Service
    {
        public class PartidoPoliticoService : GeneryService<Partido_Politico>, IPartidoPoliticoService
        {
            private readonly IGeneryRepository<Partido_Politico> _generyRepository;
            private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;

            public PartidoPoliticoService(IGeneryRepository<Partido_Politico> generyRepository, IAlianzasPoliticasRepository alianzasPoliticasRepository)
                : base(generyRepository)
            {
                _generyRepository = generyRepository;
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

            public async Task<Partido_Politico?> GetBySiglasAsync(string siglas)
            {
                if (string.IsNullOrWhiteSpace(siglas))
                {
                    throw new ArgumentException("Las siglas no pueden estar vacías.", nameof(siglas));
                }
                return await _generyRepository.GetBySiglasAsync(siglas);
            }

            public async Task<Partido_Politico?> GetPartidoConDetallesAsync(int partidoId)
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
                }

                var partido = await _generyRepository.GetByIdAsync(partidoId);
                if (partido == null)
                {
                    throw new KeyNotFoundException($"Partido político con ID {partidoId} no encontrado.");
                }


                var dirigentes = await _alianzasPoliticasRepository.GetDirigentesByPartidoAsync(partidoId);
                partido.DirigentePoliticos = dirigentes.ToList(); // Explicitly convert IEnumerable to ICollection
                return partido;
            }

            public async Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync()
            {
                var partidos = await _generyRepository.GetAllAsync();
                return partidos.Where(p => p.EsActivo);
            }

            public async Task<bool> ValidarSiglasUnicasAsync(string siglas)
            {
                if (string.IsNullOrWhiteSpace(siglas))
                {
                    throw new ArgumentException("Las siglas no pueden estar vacías.", nameof(siglas));
                }

                var existe = await _generyRepository.GetBySiglasAsync(siglas);
                return existe == null;
            }
        }
    }
}
