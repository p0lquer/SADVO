

using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class VotoRepository : GeneryRepository<Voto>, IVotoRepository
    {
        private readonly SADVOContext _context;
        public VotoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteVotoAsync(int ciudadanoId, int eleccionId)
        {
          if(ciudadanoId <= 0 || eleccionId <= 0)
            {
                throw new ArgumentException("Los IDs de ciudadano y elección deben ser mayores que cero.");
            }
            return _context.Votos.AnyAsync(v => v.CiudadanoId == ciudadanoId && v.EleccionId == eleccionId);
        }

        public Task<Voto?> GetVotoByIdAsync(int votoId)
        {

            if (votoId <= 0)
            {
                throw new ArgumentException("El ID del voto debe ser mayor que cero.", nameof(votoId));
            }
            return _context.Votos.FirstOrDefaultAsync(v => v.Id == votoId);
        }

        public async Task<IEnumerable<Voto>> GetVotosByCiudadanoAsync(int ciudadanoId)
        {
            if (ciudadanoId <= 0)
                throw new ArgumentException("El ID del ciudadano debe ser mayor que cero.", nameof(ciudadanoId));
            return await _context.Votos.Where(v => v.CiudadanoId == ciudadanoId).ToListAsync();


        }

        public async Task<IEnumerable<Voto>> GetVotosByEleccionAsync(int eleccionId)
        {
            if (eleccionId <= 0)
                throw new ArgumentException("El ID de la elección debe ser mayor que cero.", nameof(eleccionId));
            return await _context.Votos
                .Where(v => v.EleccionId == eleccionId)
                .ToListAsync();
        }

    }
}
