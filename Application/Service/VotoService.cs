
using SADVO.Domain.Interface.Repository;
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

            try
            {
                if (ciudadanoId <= 0 || eleccionId <= 0)
                {
                    throw new ArgumentException("Los IDs de ciudadano y elección deben ser mayores que cero.");
                }
                return _votoRepository.ExisteVotoAsync(ciudadanoId, eleccionId);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, logging, etc.
                throw new Exception("Error al verificar la existencia del voto.", ex);
            }
        }

        public Task<Voto?> GetVotoByIdAsync(int votoId)
        {
            try
            {
                if (votoId <= 0)
                {
                    throw new ArgumentException("El ID del voto debe ser mayor que cero.", nameof(votoId));
                }
                return _votoRepository.GetVotoByIdAsync(votoId);

            }
            catch (Exception ex)
            {
                // Manejo de excepciones, logging, etc.
                throw new Exception("Error al obtener el voto por ID.", ex);
            }
        }

        public Task<IEnumerable<Voto>> GetVotosByCiudadanoAsync(int ciudadanoId)
        {

            try
            {
                if (ciudadanoId <= 0)
                    throw new ArgumentException("El ID del ciudadano debe ser mayor que cero.", nameof(ciudadanoId));
                return _votoRepository.GetVotosByCiudadanoAsync(ciudadanoId);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, logging, etc.
                throw new Exception("Error al obtener los votos por ciudadano.", ex);
            }
        }
        public Task<IEnumerable<Voto>> GetVotosByEleccionAsync(int eleccionId)
        {
            try
            {
                if (eleccionId <= 0)
                    throw new ArgumentException("El ID de la elección debe ser mayor que cero.", nameof(eleccionId));
                return _votoRepository.GetVotosByEleccionAsync(eleccionId);
            }
            catch (Exception ex)
            {
                // Manejo de excepciones, logging, etc.
                throw new Exception("Error al obtener los votos por elección.", ex);
            }
        }
    }
}
