

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface IEleccionService : IGeneryService<Eleccion>
    {
        Task<IEnumerable<Eleccion>> GetEleccionesEnRangoFechaAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<bool> ProgramarEleccionAsync(Eleccion eleccion);
        Task<IEnumerable<Eleccion>> GetHistorialEleccionesAsync(int partidoId);
        Task<bool> ValidarFechaEleccionAsync(DateTime fecha);

    }
}
