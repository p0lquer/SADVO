using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Service
{
    public class AlianzasPoliticasService : GeneryService<Alianzas_Politica>, IAlianzasPoliticasService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        private readonly IPartidoPoliticoRepository _partidoRepository;

        public AlianzasPoliticasService(IAlianzasPoliticasRepository alianzasPoliticasRepository, IPartidoPoliticoRepository partidoPoliticoRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
            _partidoRepository = partidoPoliticoRepository;
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

        public async Task<Alianzas_Politica> CrearSolicitudAlianza(int partidoSolicitanteId, int partidoReceptorId)
        {
            var partidoSolicitante = await _partidoRepository.GetByIdAsync(partidoSolicitanteId);
            var partidoReceptor = await _partidoRepository.GetByIdAsync(partidoReceptorId);

            if (partidoSolicitante == null || partidoReceptor == null)
                throw new ArgumentException("Los partidos solicitante o receptor no existen.");

            var existeSolicitud = await _alianzasPoliticasRepository.ExisteAlianzaAsync(partidoSolicitanteId);
            if(existeSolicitud)
                throw new InvalidOperationException("Ya existe una alianza pendiente para el partido solicitante.");

 

            // Crear nueva solicitud
            var nuevaAlianza = new Alianzas_Politica
            {
                PartidoSolicitanteId = partidoSolicitanteId,
                PartidoReceptorId = partidoReceptorId,
                FechaSolicitud = DateTime.UtcNow,
                Estado = EstadoAlianza.Pendiente
            };

            await _alianzasPoliticasRepository.AddAsync(nuevaAlianza);


            return
                nuevaAlianza;
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

        public async Task<Alianzas_Politica> ResponderAlianza(int alianzaId, bool aceptar)
        {
            var alianza = await _alianzasPoliticasRepository.GetByIdAsync(alianzaId);

            if (alianza == null)
               throw new ArgumentException("La alianza no existe.", nameof(alianzaId));

            if (alianza.Estado != EstadoAlianza.Pendiente)
                throw new InvalidOperationException("La alianza ya ha sido respondida o no está pendiente.");

            alianza.Estado = aceptar ? EstadoAlianza.Aceptada : EstadoAlianza.Rechazada;
            alianza.FechaRespuesta = DateTime.UtcNow;

          await  _alianzasPoliticasRepository.UpdateAsync(alianza);


            return alianza;

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
