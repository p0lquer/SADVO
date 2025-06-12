using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Service
{
    public class PuestoElectivoService : GeneryService<Puesto_Electivo>,
        IPuestoElectivoService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public PuestoElectivoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository ?? throw new ArgumentNullException(nameof(alianzasPoliticasRepository));
        }

        public async Task<bool> ActivarDesactivarPuestoAsync(int puestoId, bool estado)
        {
            try
            {
                if (puestoId <= 0)
                {
                    throw new ArgumentException("El ID del puesto electivo debe ser mayor que cero.", nameof(puestoId));
                }
                var puesto = await _alianzasPoliticasRepository.GetByIdAsync(puestoId);
                if (puesto == null)
                {
                    throw new KeyNotFoundException($"Puesto electivo con ID {puestoId} no encontrado.");
                }
                puesto.Estado = EstadoAlianza.Pendiente;
                await _alianzasPoliticasRepository.UpdateAsync(puesto);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al activar/desactivar el puesto electivo con ID {puestoId}", ex);
            }
        }

        public async Task<Puesto_Electivo?> GetPuestoConHistorialAsync(int puestoId)
        {
            try
            {
                if (puestoId <= 0)
                {
                    throw new ArgumentException("El ID del puesto electivo debe ser mayor que cero.", nameof(puestoId));
                }
                var puesto = await _alianzasPoliticasRepository.GetByIdAsync(puestoId);
                if (puesto == null)
                {
                    throw new KeyNotFoundException($"Puesto electivo con ID {puestoId} no encontrado.");
                }
               
                return puesto;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el puesto electivo con ID {puestoId}", ex);

            }

        }
        public async Task<IEnumerable<Puesto_Electivo>> GetPuestosActivosAsync()
        {
            try
            {

                return (IEnumerable<Puesto_Electivo>)await Task.FromResult(_alianzasPoliticasRepository.GetAllAsync());
                   
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los puestos electivos activos", ex);

            }
        }

        public async Task<bool> ValidarDescripcionUnicaAsync(string descripcion)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(descripcion) || descripcion.Length > 50)
                {
                    throw new ArgumentException("La descripción no puede ser null o vacío y debe tener un máximo de 50 caracteres.", nameof(descripcion));
                }
                return await _alianzasPoliticasRepository.ExisteDescripcionAsync(descripcion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar la unicidad de la descripción: {descripcion}", ex);

            }
        }
    }
}
