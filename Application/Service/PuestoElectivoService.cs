using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Application.Service
{
    public class PuestoElectivoService : GeneryService<Puesto_Electivo>, IPuestoElectivoService
    {
        private readonly IPuestoElectivoRepository _puestoElectivoRepository;

        public PuestoElectivoService(
            IGeneryRepository<Puesto_Electivo> generyRepository,
            IPuestoElectivoRepository puestoElectivoRepository)
            : base(generyRepository)
        {
            _puestoElectivoRepository = puestoElectivoRepository ?? throw new ArgumentNullException(nameof(puestoElectivoRepository));
        }

        public async Task<bool> ActivarDesactivarPuestoAsync(int puestoId, bool estado)
        {
            try
            {
                if (puestoId <= 0)
                {
                    throw new ArgumentException("El ID del puesto electivo debe ser mayor que cero.", nameof(puestoId));
                }

                var puesto = await _puestoElectivoRepository.GetByIdAsync(puestoId);
                if (puesto == null)
                {
                    throw new KeyNotFoundException($"Puesto electivo con ID {puestoId} no encontrado.");
                }

                // Cambiar el estado activo del puesto
                puesto.EsActivo = estado;
                await _puestoElectivoRepository.UpdateAsync(puesto);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al activar/desactivar el puesto electivo con ID {puestoId}", ex);
            }
        }

        public async Task<IEnumerable<Puesto_Electivo>> GetPuestosActivosAsync()
        {
            try
            {
                // Usar el método específico del repositorio que ya filtra por activos
                return await _puestoElectivoRepository.GetPuestosActivosAsync();
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
                if (string.IsNullOrWhiteSpace(descripcion))
                {
                    throw new ArgumentException("La descripción no puede ser null o vacía.", nameof(descripcion));
                }

                if (descripcion.Length > 50)
                {
                    throw new ArgumentException("La descripción debe tener un máximo de 50 caracteres.", nameof(descripcion));
                }

                // Retorna true si existe (para validar unicidad, normalmente querrías retornar !existe)
                return await _puestoElectivoRepository.ExisteDescriptionAsync(descripcion);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al validar la unicidad de la descripción: {descripcion}", ex);
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

                // Usar el método del repositorio que incluye las elecciones (historial)
                return await _puestoElectivoRepository.GetPuestoWithEleccionesAsync(puestoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al obtener el puesto electivo con historial. ID: {puestoId}", ex);
            }
        }
    }
}