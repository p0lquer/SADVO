

using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class PartidoPoliticoRepository : GeneryRepository<Partido_Politico>, IPartidoPoliticoRepository
    {
        private readonly SADVOContext _context;
        public PartidoPoliticoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool>ExisteSiglasAsync(string siglas)
        {
            
            if(string.IsNullOrWhiteSpace(siglas))
            {
                throw new ArgumentException("Siglas cannot be null or empty.", nameof(siglas));
            }
            try
            {
                return Task.FromResult(_context.PartidosPoliticos.Any(p => p.Siglas.Equals(siglas, StringComparison.OrdinalIgnoreCase)));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking if party with siglas '{siglas}' exists", ex);
            }
        }

     
        public Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync()
        {
            try
            {
               var activeParties = _context.PartidosPoliticos
                    .Where(p => p.EsActivo == true)
                    .ToList();
                return Task.FromResult<IEnumerable<Partido_Politico>>(activeParties);
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active parties", ex);
            }
        }

        public async Task<List<Partido_Politico?>> GetPartidoWithDirigentesAsync(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("Partido ID must be greater than zero, or not Found.", nameof(partidoId));
                }
                var  partido =  await _context.PartidosPoliticos
                     .Include(p => p.DirigentePoliticos)
                     .FirstOrDefaultAsync(p => p.Id == partidoId);

                if (partido == null)
                {
                    return new List<Partido_Politico?>();
                }
                return new List<Partido_Politico?> { partido };


            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving party with ID {partidoId} and its leaders", ex);
            }

        }
        public async Task<Partido_Politico> GetBySiglasAsync(string siglas)
        {
            if (string.IsNullOrWhiteSpace(siglas))
            {
                throw new ArgumentException("Siglas cannot be null or empty.", nameof(siglas));
            }

            try
            {
                var partido = await _context.PartidosPoliticos
                    .FirstOrDefaultAsync(p => p.Siglas.Equals(siglas, StringComparison.OrdinalIgnoreCase));

                if (partido == null)
                {
                    throw new InvalidOperationException($"No party found with siglas '{siglas}'.");
                }

                return partido;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving party with siglas '{siglas}'", ex);
            }
        }
    }
}

