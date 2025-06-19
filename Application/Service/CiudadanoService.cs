using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Application.DTOs.Ciudadano;

namespace SADVO.Application.Service
{
    public class CiudadanoService : GeneryService<Ciudadano>, ICiudadanoService
    {
        private readonly ICiudadanoRepository _ciudadanoRepository;

        public CiudadanoService(ICiudadanoRepository ciudadanoRepository) : base(ciudadanoRepository)
        {
            _ciudadanoRepository = ciudadanoRepository;
        }

        public async Task<bool> ActivarDesactivarCiudadanoAsync(int ciudadanoId, bool estado)
        {
            try
            {
                if (ciudadanoId <= 0)
                {
                    throw new ArgumentException("El ID del ciudadano debe ser mayor que cero.", nameof(ciudadanoId));
                }

                var ciudadano = await _ciudadanoRepository.GetByIdAsync(ciudadanoId);
                if (ciudadano == null)
                {
                    throw new KeyNotFoundException($"Ciudadano con ID {ciudadanoId} no encontrado.");
                }

                ciudadano.EsActivo = estado;
                await _ciudadanoRepository.UpdateAsync(ciudadano);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al activar/desactivar el ciudadano con ID {ciudadanoId}", ex);
            }
        }

        public async Task<IEnumerable<CiudadanoDto>> BuscarCiudadanosAsync(string criterio)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(criterio))
                {
                    throw new ArgumentException("El criterio de búsqueda no puede ser nulo o vacío.", nameof(criterio));
                }

                var ciudadanos = await _ciudadanoRepository.GetAllAsync();

                return ciudadanos
                    .Where(c => c.Nombre.Contains(criterio, StringComparison.OrdinalIgnoreCase) ||
                                c.Apellido.Contains(criterio, StringComparison.OrdinalIgnoreCase) ||
                                c.NumeroIdentificacion.Contains(criterio, StringComparison.OrdinalIgnoreCase))
                    .Select(c => new CiudadanoDto
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        Apellido = c.Apellido,
                        NumeroIdentificacion = c.NumeroIdentificacion,
                        Email = c.Email,
                        EsActivo = c.EsActivo
                    })
                    .ToList();
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
                return await _ciudadanoRepository.GetByNumeroIdentificacionAsync(numeroIdentificacion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving citizen by identification number {numeroIdentificacion}", ex);
            }
        }

        public async Task<Ciudadano?> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await _ciudadanoRepository.GetByEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving citizen by email {email}", ex);
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

                var existeNumero = await _ciudadanoRepository.ExisteNumeroIdentificacionAsync(numeroIdentificacion);
                var existeEmail = await _ciudadanoRepository.ExisteEmailAsync(email);

                return !existeNumero && !existeEmail;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating unique citizen with identification number {numeroIdentificacion} and email {email}", ex);
            }
        }

        public async Task<IEnumerable<Ciudadano>> GetCiudadanosActivosAsync()
        {
            try
            {
                return await _ciudadanoRepository.GetCiudadanosActivosAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active citizens", ex);
            }
        }

        public async Task AddAsync(Ciudadano ciudadano)
        {
            try
            {
                if (ciudadano == null)
                {
                    throw new ArgumentNullException(nameof(ciudadano), "El ciudadano no puede ser null.");
                }

                await _ciudadanoRepository.AddAsync(ciudadano);
            }
            catch (Exception ex)
            {
                throw new Exception("Error adding a new citizen", ex);
            }
        }

      
    }
}