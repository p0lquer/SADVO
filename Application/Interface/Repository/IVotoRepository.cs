

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Repository
{
    public interface IVotoRepository : IGeneryRepository<Voto>
    {
        Task<IEnumerable<Voto>> GetVotosByEleccionAsync(int eleccionId);

        Task<IEnumerable<Voto>> GetVotosByCiudadanoAsync(int ciudadanoId);

        Task<bool> ExisteVotoAsync(int ciudadanoId, int eleccionId);

        Task<Voto?> GetVotoByIdAsync(int votoId);
    }
}
