

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface IPuestoElectivoService : IGeneryService<Puesto_Electivo>
    {
        Task<IEnumerable<Puesto_Electivo>> GetPuestosActivosAsync();
        Task<bool> ActivarDesactivarPuestoAsync(int puestoId, bool estado);
        Task<bool> ValidarDescripcionUnicaAsync(string descripcion);
        Task<Puesto_Electivo?> GetPuestoConHistorialAsync(int puestoId);

    }
}
