

using Microsoft.EntityFrameworkCore;
using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class AlianzasPoliticasRepository : GeneryRepository<Alianzas_Politica>, IAlianzasPoliticasRepository
    {
        private readonly SADVOContext _context;
        public AlianzasPoliticasRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));

        }

        public Task<bool> ExisteAlianzaAsync(int partidoId)
        {

            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
                }
                return Task.FromResult(_context.AlianzasPoliticas.Any(a => a.PartidoSolicitanteId == partidoId || a.PartidoReceptorId == partidoId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if alliance exists for party with ID {partidoId}", ex);
            }
        }

        public async Task<bool> ExisteDescripcionAsync(string descripcion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(descripcion) || descripcion.Length > 100)
                {
                    throw new ArgumentException("La descripción no puede ser null o vacío y debe tener un máximo de 100 caracteres.", nameof(descripcion));
                }
                return await Task.FromResult(_context.AlianzasPoliticas.Any(a => a.Descripcion.Equals(descripcion, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if alliance with description '{descripcion}' exists", ex);

            }
        }

        public async Task<bool> ExisteEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await Task.FromResult(_context.AlianzasPoliticas.Any(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if alliance with email '{email}' exists", ex);

            }
        }

        public Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId)
        {
            if (partidoId > 0)
            {
                try
                {
                    return Task.FromResult(_context.AlianzasPoliticas
                        .Where(a => a.PartidoSolicitanteId == partidoId || a.PartidoReceptorId == partidoId)
                        .AsEnumerable());

                }
                catch (Exception ex)
                {
                    throw new Exception($"Error retrieving alliances for party with ID {partidoId}", ex);
                }
            }
            else
            {
                throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
            }
        }

        public async Task GetByEmailAsync(string email)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(email) || email.Length > 50)
                {
                    throw new ArgumentException("El email no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(email));
                }
                return await Task.FromResult(_context.AlianzasPoliticas.FirstOrDefault(a => a.Email.Equals(email, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving alliance by email {email}", ex);

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
                return await Task.FromResult(_context.Ciudadanos.FirstOrDefault(c => c.NumeroIdentificacion.Equals(numeroIdentificacion, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving citizen by identification number {numeroIdentificacion}", ex);

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
                return await Task.FromResult(_context.DirigentesPoliticos
                    .Where(d => d.PartidoPoliticoId == partidoPoliticoId)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving political leaders for party with ID {partidoPoliticoId}", ex);

            }
        }

        public async Task GetDirigentesByUsuarioAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new ArgumentException("El ID del usuario debe ser mayor que cero.", nameof(usuarioId));
                }
                 await Task.FromResult(_context.DirigentesPoliticos
                    .Where(d => d.UsuarioId == usuarioId)
                    .AsEnumerable());
                
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving political leaders for user with ID {usuarioId}", ex);

            }
        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin)
        {
            try
            {
                if (fechaInicio == default || fechaFin == default)
                {
                    throw new ArgumentException("Las fechas de inicio y fin no pueden ser nulas.");
                }
                if (fechaInicio > fechaFin)
                {
                    throw new ArgumentException("La fecha de inicio no puede ser posterior a la fecha de fin.");
                }
                return await Task.FromResult(_context.Elecciones
                    .Where(e => e.FechaOcurrida >= fechaInicio && e.FechaOcurrida <= fechaFin)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving elections between {fechaInicio} and {fechaFin}", ex);
            }

        }

        public async Task<IEnumerable<Eleccion>> GetEleccionesByPartidoAsync(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido político debe ser mayor que cero.", nameof(partidoId));
                }
                return await Task.FromResult(_context.Elecciones
                    .Where(e => e.Id == partidoId)
                    .AsEnumerable());
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving elections for party with ID {partidoId}", ex);

            }
        }

        public async Task<Usuarios?> GetUsuarioByEmailAsync(string email)
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

        public async Task<Usuarios?> GetUsuarioConRolesAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new ArgumentException("El ID del usuario debe ser mayor que cero.", nameof(usuarioId));
                }
                return await Task.FromResult(_context.Usuarios
                    .Include(u => u.RolUsuario)
                    .FirstOrDefault(u => u.Id == usuarioId));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving user with roles for user ID {usuarioId}", ex);

            }
        }
    }
}
