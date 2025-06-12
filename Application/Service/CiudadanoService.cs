using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Service
{
    public class CiudadanoService : GeneryService<Ciudadano>, ICiudadanoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public CiudadanoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public async Task<bool> ActivarDesactivarCiudadanoAsync(int ciudadanoId, bool estado)
        {
            try
            {
                if (ciudadanoId <= 0)
                {
                    throw new ArgumentException("El ID del ciudadano debe ser mayor que cero.", nameof(ciudadanoId));
                }
                var ciudadano = await _alianzasPoliticasRepository.GetByIdAsync(ciudadanoId);
                if (ciudadano == null)
                {
                    throw new KeyNotFoundException($"Ciudadano con ID {ciudadanoId} no encontrado.");
                }
                ciudadano.Estado = EstadoAlianza.Pendiente;
                await _alianzasPoliticasRepository.UpdateAsync(ciudadano);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al activar/desactivar el ciudadano con ID {ciudadanoId}", ex);

            }
        }

        public async Task<IEnumerable<Ciudadano>> BuscarCiudadanosAsync(string criterio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(criterio))
                {
                    throw new ArgumentException("El criterio de búsqueda no puede ser nulo o vacío.", nameof(criterio));
                }

                var ciudadanos = await _alianzasPoliticasRepository.GetAllAsync();

                return ciudadanos.Where(c => c.Nombre.Contains(criterio, StringComparison.OrdinalIgnoreCase) ||
                                             c.Apellido.Contains(criterio, StringComparison.OrdinalIgnoreCase) ||
                                             c.NumeroIdentificacion.Contains(criterio, StringComparison.OrdinalIgnoreCase));

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al buscar ciudadanos con el criterio '{criterio}'", ex);
            }

        }

        public async Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroIdentificacion) || numeroIdentificacion.Length > 12)
                {
                    throw new ArgumentException("El número de identificación no puede ser null o vacío y debe tener un máximo de 12 caracteres.", nameof(numeroIdentificacion));
                }
                return await _alianzasPoliticasRepository.GetByNumeroIdentificacionAsync(numeroIdentificacion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving citizen by identification number {numeroIdentificacion}", ex);

            }
        }

        public async Task<bool> ValidarCiudadanoUnicoAsync(string numeroIdentificacion, string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(numeroIdentificacion) || numeroIdentificacion.Length > 12)
                {
                    throw new ArgumentException("El número de identificación no puede ser null o vacío y debe tener un máximo de 12 caracteres.", nameof(numeroIdentificacion));
                }
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                var ciudadanoPorNumero = await _alianzasPoliticasRepository.GetByNumeroIdentificacionAsync(numeroIdentificacion);
                if (ciudadanoPorNumero == null)
                {
                    throw new KeyNotFoundException($"Ciudadano con número de identificación {numeroIdentificacion} no encontrado.");
                }
                var ciudadanoPorEmail = await _alianzasPoliticasRepository.GetByEmailAsync(email);
                return ciudadanoPorNumero == null && ciudadanoPorEmail == null;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating unique citizen with identification number {numeroIdentificacion} and email {email}", ex);
            }

        }
    }
}
