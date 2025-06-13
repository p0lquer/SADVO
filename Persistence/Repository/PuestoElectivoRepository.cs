

using Microsoft.EntityFrameworkCore;
using SADVO.Domain.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class PuestoElectivoRepository : GeneryRepository<Puesto_Electivo>, IPuestoElectivoRepository
    {
        private readonly SADVOContext _context;
        public PuestoElectivoRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteDescriptionAsync(string description)
        {
            if (string.IsNullOrWhiteSpace(description))
            {
                throw new ArgumentException("Description cannot be null or empty.", nameof(description));
            }
            try
            {
                var desc = _context.PuestosElectivos
                    .Any(p => p.Description.Equals(description, StringComparison.OrdinalIgnoreCase));
                return Task.FromResult(desc);
            }
            catch(Exception ex)
            {
                throw new Exception("Error checking if puesto exists by description", ex);
            }
            
        }

        public Task<IEnumerable<Puesto_Electivo>> GetPuestosActivosAsync()
        {
            try
            {
                var puestosActivos = _context.PuestosElectivos
                    .Where(p => p.EsActivo == true)
                    .AsEnumerable();
                return Task.FromResult(puestosActivos);

            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active puestos", ex);
            }
        }

        public Task<Puesto_Electivo?> GetPuestoWithEleccionesAsync(int puestoId)
        {
            if(puestoId <= 0)
            {
                throw new ArgumentException("Puesto ID must be greater than zero.", nameof(puestoId));
            }
            try
            {
                var puesto = _context.PuestosElectivos
                    .Include(p => p.Eleccion)
                    .FirstOrDefaultAsync(p => p.Id == puestoId);
                return puesto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving puesto with ID {puestoId}", ex);
            }
        }
    }
}
