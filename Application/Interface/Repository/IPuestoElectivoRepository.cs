

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface IPuestoElectivoRepository : IGeneryRepository<Puesto_Electivo>
    {
        Task<IEnumerable<Puesto_Electivo>> GetPuestosActivosAsync();

        Task<Puesto_Electivo?> GetPuestoWithEleccionesAsync(int puestoId);

        Task<bool> ExisteDescriptionAsync(string description);

    }
}
