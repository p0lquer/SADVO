using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class DirigentePoliticoService : GeneryService<Dirigente_Politico>, IDirigentePoliticoService
    {
        private readonly IDirigentePoliticoRepository _dirigenteRepository; // CORREGIDO: Usar la interfaz específica

        public DirigentePoliticoService(IDirigentePoliticoRepository dirigenteRepository) // CORREGIDO: Inyectar repositorio específico
            : base(dirigenteRepository)
        {
            _dirigenteRepository = dirigenteRepository ?? throw new ArgumentNullException(nameof(dirigenteRepository));
        }

        public async Task<bool> AsignarDirigenteAPartidoAsync(int usuarioId, int partidoPoliticoId)
        {
            try
            {
                if (usuarioId <= 0 || partidoPoliticoId <= 0)
                {
                    throw new ArgumentException("Los IDs de usuario y partido político deben ser mayores que cero.");
                }

                // Verificar si ya existe el dirigente para evitar duplicados
                var dirigenteExistente = await ValidarDirigenteExistenteAsync(usuarioId, partidoPoliticoId);
                if (dirigenteExistente)
                {
                    return false; // Ya existe
                }

                // Crear la entidad Dirigente_Politico
                var dirigenteEntity = new Dirigente_Politico
                {
                    UsuarioId = usuarioId,
                    PartidoPoliticoId = partidoPoliticoId
                };

                // Guardar en el repositorio
                var resultado = await _dirigenteRepository.AddAsync(dirigenteEntity);
                return resultado != null;
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

                return await _dirigenteRepository.GetDirigentesByPartidoAsync(partidoPoliticoId);
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

                // Buscar dirigentes por partido y filtrar por usuario
                var dirigentesDelPartido = await _dirigenteRepository.GetDirigentesByPartidoAsync(partidoPoliticoId);
                var dirigente = dirigentesDelPartido?.FirstOrDefault(d => d.UsuarioId == usuarioId);

                if (dirigente != null)
                {
                    // Remover el dirigente del repositorio
                    var resultado = await _dirigenteRepository.DeleteAsync(dirigente.Id);
                    return resultado;
                }
                return false;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing political leader from party with user ID {usuarioId} and party ID {partidoPoliticoId}", ex);
            }
        }

        public async Task<bool> ValidarDirigenteExistenteAsync(int usuarioId, int partidoPoliticoId)
        {
            try
            {
                if (usuarioId <= 0 || partidoPoliticoId <= 0)
                {
                    throw new ArgumentException("Los IDs de usuario y partido político deben ser mayores que cero.");
                }

                // CORREGIDO: Usar el método específico del repositorio
                return await _dirigenteRepository.ExisteDirigenteAsync(usuarioId, partidoPoliticoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating existing political leader with user ID {usuarioId} and party ID {partidoPoliticoId}", ex);
            }
        }

        // MÉTODOS ADICIONALES que podrías agregar a la interfaz

        public async Task<IEnumerable<Dirigente_Politico>> GetDirigentesByUsuarioAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new ArgumentException("El ID del usuario debe ser mayor que cero.", nameof(usuarioId));
                }

                return await _dirigenteRepository.GetDirigentesByUsuarioAsync(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving political leaders for user with ID {usuarioId}", ex);
            }
        }

        public async Task<Dirigente_Politico?> GetDirigenteWithDetailsAsync(int dirigenteId)
        {
            try
            {
                if (dirigenteId <= 0)
                {
                    throw new ArgumentException("El ID del dirigente debe ser mayor que cero.", nameof(dirigenteId));
                }

                return await _dirigenteRepository.GetDirigenteWithDetailsAsync(dirigenteId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving political leader details for dirigente ID {dirigenteId}", ex);
            }
        }
    }
}