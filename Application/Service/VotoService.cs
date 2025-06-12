
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{

    public class VotoService : GeneryService<Voto>, IVotoService
    {
        private readonly IVotoRepository _votoRepository;
        public VotoService(IVotoRepository votoRepository) : base(votoRepository)
        {
             _votoRepository = votoRepository;
        }

        public Task<bool> ExisteVotoAsync(int ciudadanoId, int eleccionId)
        {
            throw new NotImplementedException();
        }

        public Task<Voto?> GetVotoByIdAsync(int votoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Voto>> GetVotosByCiudadanoAsync(int ciudadanoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Voto>> GetVotosByEleccionAsync(int eleccionId)
        {
            throw new NotImplementedException();
        }
    }
}
