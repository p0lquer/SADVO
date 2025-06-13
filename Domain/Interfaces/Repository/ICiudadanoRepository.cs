

using SADVO.Domain.Entities;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Domain.Interface.Repository
{
    public interface ICiudadanoRepository : IGeneryRepository<Ciudadano>
    {
        Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion);

        Task<Ciudadano?> GetByEmailAsync(string email);

        Task<IEnumerable<Ciudadano>> GetCiudadanosActivosAsync();

        Task<bool> ExisteNumeroIdentificacionAsync(string numeroIdentificacion);

        Task<bool> ExisteEmailAsync(string email);

    }
}
