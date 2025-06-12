

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface IVotoService : IGeneryService<Voto>
    {
        public Task<IEnumerable<Voto>> GetVotosByEleccionAsync(int eleccionId);

        public Task<IEnumerable<Voto>> GetVotosByCiudadanoAsync(int ciudadanoId);

        public Task<bool> ExisteVotoAsync(int ciudadanoId, int eleccionId);

        public Task<Voto?> GetVotoByIdAsync(int votoId);



    }
}
