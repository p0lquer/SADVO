using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class UsuariosService : GeneryService<Usuarios>, IUsuariosService
    {
        private readonly IUsuariosRepository _usuariosRepository;
        public UsuariosService(IUsuariosRepository usuariosRepository) : base(usuariosRepository)
        {
            _usuariosRepository = usuariosRepository;
        }

        public async Task<Usuarios?> AutenticarUsuarioAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
                {
                    throw new ArgumentException("El email y la contraseña no pueden ser nulos o vacíos.");
                }
                var usuario = await _usuariosRepository.GetByEmailAsync(email); // Correct method name
                if (usuario == null)
                {
                    return null; // Usuario no encontrado
                }
                // Aquí deberías implementar la lógica para verificar la contraseña
                // Por ejemplo, si estás usando un hash, deberías comparar el hash de la contraseña proporcionada con el almacenado
                return usuario; // Usuario autenticado correctamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al autenticar al usuario.", ex);
            }
        }

        public async Task<bool> CambiarPasswordAsync(int usuarioId, string passwordActual, string passwordNuevo)
        {

            try
            {
                if (usuarioId <= 0 || string.IsNullOrWhiteSpace(passwordActual) || string.IsNullOrWhiteSpace(passwordNuevo))
                {
                    throw new ArgumentException("El ID de usuario, la contraseña actual y la nueva no pueden ser nulos o vacíos.");
                }
                var usuario = await _usuariosRepository.GetByIdAsync(usuarioId);
                if (usuario == null)
                {
                    return false; // Usuario no encontrado
                }
                // Aquí deberías implementar la lógica para cambiar la contraseña
                // Por ejemplo, si estás usando un hash, deberías actualizar el hash de la contraseña
                return true; // Contraseña cambiada correctamente
            }
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar la contraseña del usuario.", ex);
            }
        }

        public async Task<Usuarios?> GetUsuarioConRolesAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new ArgumentException("El ID de usuario debe ser mayor que cero.", nameof(usuarioId));
                }
                // Updated method call to match the repository interface
                return await _usuariosRepository.GetUsuarioWithDirigentesAsync(usuarioId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user with roles for user ID {usuarioId}", ex);
            }
        }

        public async Task<bool> ValidarEmailUnicoAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await _usuariosRepository.ExisteEmailAsync(email);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating unique email {email}", ex);

            }
        }

        public async Task<bool> ValidarPasswordsCoincidanAsync(string password, string confirmationPassword)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmationPassword))
                {
                    throw new ArgumentException("La contraseña y la confirmación no pueden ser nulas o vacías.");
                }
                return password == confirmationPassword;
            }
            catch (Exception ex)
            {
                throw new Exception("Error validating password match.", ex);

            }
        }
    }
}