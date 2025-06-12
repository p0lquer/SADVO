using Microsoft.EntityFrameworkCore;
using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class DirigentePoliticoRepository : GeneryRepository<Dirigente_Politico>, IDirigentePoliticoRepository
    {
        private readonly SADVOContext _context;

        public DirigentePoliticoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<bool> ExisteDirigenteAsync(int usuarioId, int partidoPoliticoId)
        {
            try
            {
                if (usuarioId <= 0 || partidoPoliticoId <= 0)
                {
                    throw new ArgumentException("Los IDs de usuario y partido político deben ser mayores que cero.");
                }

                // CORREGIDO: Usar AnyAsync en lugar de Task.FromResult
                return await _context.DirigentesPoliticos
                    .AnyAsync(d => d.UsuarioId == usuarioId && d.PartidoPoliticoId == partidoPoliticoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if political leader exists for user ID {usuarioId} and party ID {partidoPoliticoId}", ex);
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

                // CORREGIDO: Usar ToListAsync y agregar Include para cargar entidades relacionadas
                return await _context.DirigentesPoliticos
                    .Where(d => d.PartidoPoliticoId == partidoPoliticoId)
                    .Include(d => d.Usuario)
                    .Include(d => d.PartidoPolitico)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving political leaders for party with ID {partidoPoliticoId}", ex);
            }
        }

        public async Task<IEnumerable<Dirigente_Politico>> GetDirigentesByUsuarioAsync(int usuarioId)
        {
            try
            {
                if (usuarioId <= 0)
                {
                    throw new ArgumentException("El ID del usuario debe ser mayor que cero.", nameof(usuarioId));
                }

                // CORREGIDO: Usar ToListAsync y agregar Include
                return await _context.DirigentesPoliticos
                    .Where(d => d.UsuarioId == usuarioId)
                    .Include(d => d.Usuario)
                    .Include(d => d.PartidoPolitico)
                    .ToListAsync();
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

                // CORREGIDO: Usar FirstOrDefaultAsync
                return await _context.DirigentesPoliticos
                    .Include(d => d.Usuario)
                    .Include(d => d.PartidoPolitico)
                    .FirstOrDefaultAsync(d => d.Id == dirigenteId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving political leader with ID {dirigenteId}", ex);
            }
        }
    }
}