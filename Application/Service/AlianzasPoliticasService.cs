using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class AlianzasPoliticasService : GeneryService<Alianzas_Politica>, IAlianzasPoliticasServicen
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;

        public AlianzasPoliticasService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base( alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public async Task<bool> CrearAlianzaAsync(Alianzas_Politica alianza)
        {
            try
            {
                if (alianza == null)
                    throw new ArgumentNullException(nameof(alianza), "La alianza no puede ser nula.");
                if (alianza.PartidoSolicitanteId <= 0 || alianza.PartidoReceptorId <= 0)
                    throw new ArgumentException("Los IDs de los partidos deben ser mayores que cero.");
                 await _alianzasPoliticasRepository.AddAsync(alianza);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al crear la alianza política.", ex);

            }
        }

        public async Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));

                return await _alianzasPoliticasRepository.GetAlianzasByPartidoAsync(partidoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener alianzas para el partido con ID {partidoId}.", ex);

            }
        }

        public async Task<bool> ValidarAlianzaExistenteAsync(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
                return await _alianzasPoliticasRepository.ExisteAlianzaAsync(partidoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar si existe una alianza para el partido con ID {partidoId}.", ex);

            }
        }
        
    }
    
}
