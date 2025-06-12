using Microsoft.EntityFrameworkCore;
using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class UsuariosRepository : GeneryRepository<Usuarios>, IUsuariosRepository
    {
        private readonly SADVOContext _context;
        public UsuariosRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await Task.FromResult(_context.Usuarios.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if user exists by email {email}", ex);

            }
        }

        public async Task<Usuarios?> GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await Task.FromResult(_context.Usuarios.FirstOrDefault(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user by email {email}", ex);

            }
        }

        public async Task<IEnumerable<Usuarios>> GetUsuariosByApellidoAsync(string apellido)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apellido) || apellido.Length > 12)
                {
                    throw new ArgumentException("El apellido no puede ser null o vacío y debe tener un máximo de 12 caracteres.", nameof(apellido));
                }
                return await Task.FromResult(_context.Usuarios.Where(u => u.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase)).AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving users by last name {apellido}", ex);

            }
        }

        public async Task<Usuarios?> GetUsuarioWithDirigentesAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new ArgumentException("El ID del usuario debe ser mayor que cero.", nameof(usuarioId));
                }
                return await Task.FromResult(_context.Usuarios
                    .Include(u => u.DirigentePoliticos)
                    .FirstOrDefault(u => u.Id == usuarioId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user with leaders for user ID {usuarioId}", ex);

            }
        }

        public async Task<bool> ValidarCredencialesAsync(string email, string password)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                if (string.IsNullOrWhiteSpace(password) || password.Length > 20)
                {
                    throw new ArgumentException("La contraseña no puede ser null o vacía y debe tener un máximo de 20 caracteres.", nameof(password));
                }

                return await Task.FromResult(_context.Usuarios.Any(u => u.Email.Equals(email, StringComparison.OrdinalIgnoreCase) && u.Password == password));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating credentials for email {email}", ex);

            }
        }
    }
}
