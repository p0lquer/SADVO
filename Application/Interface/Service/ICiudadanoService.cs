

using SADVO.Application.DTOs.Ciudadano;
using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface ICiudadanoService : IGeneryService<Ciudadano>
    {
        Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion);
        Task<bool> ValidarCiudadanoUnicoAsync(string numeroIdentificacion, string email);
        Task<bool> ActivarDesactivarCiudadanoAsync(int ciudadanoId, bool estado);
        Task<IEnumerable<CiudadanoDto>> BuscarCiudadanosAsync(string criterio);
        Task<IEnumerable<Ciudadano>> GetCiudadanosActivosAsync(); 
        Task AddAsync(Ciudadano ciudadano);
    }
}
