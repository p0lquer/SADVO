

using SADVO.Application.Interface.Repository;
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
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Puesto_Electivo>> GetPuestosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Puesto_Electivo?> GetPuestoWithEleccionesAsync(int puestoId)
        {
            throw new NotImplementedException();
        }
    }
}
