

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface ICiudadanoService : IGeneryService<Ciudadano>
    {
        Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion);
        Task<bool> ValidarCiudadanoUnicoAsync(string numeroIdentificacion, string email);
        Task<bool> ActivarDesactivarCiudadanoAsync(int ciudadanoId, bool estado);
        Task<IEnumerable<Ciudadano>> BuscarCiudadanosAsync(string criterio);
        Task<IEnumerable<Ciudadano>> GetCiudadanosActivosAsync(); // Added this method

        Task AddAsync(Ciudadano ciudadano);
    }
}
