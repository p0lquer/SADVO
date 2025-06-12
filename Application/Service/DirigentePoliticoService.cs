
using SADVO.Application.DTOs.AsignacionDirigente;
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class DirigentePoliticoService : GeneryService<Dirigente_Politico>, IDirigentePoliticoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public DirigentePoliticoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public async Task<bool> AsignarDirigenteAPartidoAsync(int usuarioId, int partidoPoliticoId)
        {
            try
            {
                if (usuarioId <= 0 || partidoPoliticoId <= 0)
                {
                    throw new ArgumentException("Los IDs de usuario y partido político deben ser mayores que cero.");
                }
                var dirigente = new DirigentePoliticoDto
                {
                    UsuarioId = usuarioId,
                    PartidoPoliticoId = partidoPoliticoId,
                    PartidoPolitico = (DirigentePoliticoDto)await _alianzasPoliticasRepository.GetAlianzasByPartidoAsync(partidoPoliticoId)
                };
                
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error assigning political leader to party with user ID {usuarioId} and party ID {partidoPoliticoId}", ex);

            }
        }

        public async Task<IEnumerable<Dirigente_Politico>> GetDirigentesByPartidoAsync(int partidoPoliticoId)
        {
            try
            {
                if (partidoPoliticoId <= 0)
                {
                    throw new ArgumentException("El ID del partido político debe ser mayor que cero.", nameof(partidoPoliticoId));
                }
                return await _alianzasPoliticasRepository.GetDirigentesByPartidoAsync(partidoPoliticoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving political leaders for party with ID {partidoPoliticoId}", ex);
            }
        }
        public async Task<bool> RemoverDirigenteDePartidoAsync(int usuarioId, int partidoPoliticoId)
        {
            try
            {
                if (usuarioId <= 0 || partidoPoliticoId <= 0)
                {
                    throw new ArgumentException("Los IDs de usuario y partido político deben ser mayores que cero.");
                }
                var dirigente = await _alianzasPoliticasRepository.GetDirigentesByUsuarioAsync(usuarioId);
                if (dirigente != null && dirigente.PartidoPoliticoId == partidoPoliticoId)
                {

                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing political leader from party with user ID {usuarioId} and party ID {partidoPoliticoId}", ex);
            }
        }

        public Task<bool> ValidarDirigenteExistenteAsync(int usuarioId, int partidoPoliticoId)
        {
            throw new NotImplementedException();
        }
    }
}
