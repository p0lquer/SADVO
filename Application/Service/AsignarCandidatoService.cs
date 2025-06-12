using SADVO.Application.DTOs.AsignacionDirigente;
using SADVO.Application.DTOs.Candidato;
using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Service
{
    public class AsignarCandidatoService : GeneryService<Asignar_Candidato>, IAsignarCandidatoService
    {
        private readonly IAsignarCandidatoService _service;

        public AsignarCandidatoService(IAsignarCandidatoService service, IGeneryRepository<Asignar_Candidato> generyRepository) : base(generyRepository)
        {
            _service = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<bool> AsignarCandidatoAPuestoAsync(int candidatoId, int puestoElectivoId, TypeCandidate tipoCandidato)
        {
            try
            {
                if (candidatoId <= 0 || puestoElectivoId <= 0)
                {
                    throw new ArgumentException("Los IDs de candidato y puesto electivo deben ser mayores que cero.");
                }
                if (await _service.ValidarAsignacionDuplicadaAsync(candidatoId, puestoElectivoId))
                {
                    throw new InvalidOperationException("La asignación ya existe.");
                }
                var asignacion = new AsignacionCandidatoDto
                {
                    CandidatoId = candidatoId,
                    PuestoElectivoId = puestoElectivoId,
                    TipoCandidato = tipoCandidato,
                };
                await _service.CreateAsync( asignacion);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al asignar candidato con ID {candidatoId} al puesto con ID {puestoElectivoId}", ex);

            }

        }

        public  async Task<Asignar_Candidato> CreateAsync(Asignar_Candidato entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                await _service.CreateAsync(entity);
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating assignment", ex);
            }
        }

        public async Task CreateAsync(AsignacionCandidatoDto asignacion)
        {
            try
            {
                if (asignacion == null)
                    throw new ArgumentNullException(nameof(asignacion), "Asignacion cannot be null.");

                var entity = new AsignacionCandidatoDto
                {
                    CandidatoId = asignacion.CandidatoId,
                    PuestoElectivoId = asignacion.PuestoElectivoId,
                    TipoCandidato = asignacion.TipoCandidato
                };

                await _service.CreateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating assignment", ex);
            }
        }

        public async Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByCandidatoAsync(int candidatoId)
        {
            try
            {
                if (candidatoId <= 0)
                {
                    throw new ArgumentException("El ID del candidato debe ser mayor que cero.", nameof(candidatoId));
                }
                return await _service.GetAsignacionesByCandidatoAsync(candidatoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assignments for candidate with ID {candidatoId}", ex);

            }
        }

        public async Task<bool> RemoverAsignacionAsync(int candidatoId, int puestoElectivoId)
        {
            try
            {
                if (candidatoId <= 0 || puestoElectivoId <= 0)
                {
                    throw new ArgumentException("Los IDs de candidato y puesto electivo deben ser mayores que cero.");
                }
                var asignacion = await _service.GetAsignacionesByCandidatoAsync(candidatoId);
                if (asignacion == null || !asignacion.Any(a => a.PuestoElectivoId == puestoElectivoId))
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }
                 await _service.RemoverAsignacionAsync(candidatoId, puestoElectivoId);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing assignment for candidate ID {candidatoId} and position ID {puestoElectivoId}", ex);
            }
        }

        public async Task<Asignar_Candidato> UpdateAsync(Asignar_Candidato entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                return await _service.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating assignment", ex);
            }

        }

        public async Task<bool> ValidarAsignacionDuplicadaAsync(int candidatoId, int puestoElectivoId)
        {

            try
            {
                if (candidatoId <= 0 || puestoElectivoId <= 0)
                {
                    throw new ArgumentException("Los IDs de candidato y puesto electivo deben ser mayores que cero.");
                }
                return await _service.ValidarAsignacionDuplicadaAsync(candidatoId, puestoElectivoId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error checking for duplicate assignment for candidate ID {candidatoId} and position ID {puestoElectivoId}", ex);
            }
        }

        Task<IEnumerable<Asignar_Candidato>> IGeneryService<Asignar_Candidato>.GetAllAsync()
        {
            try
            {
                return _service.GetAllAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving all assignments", ex);
            }
        }

        Task<Asignar_Candidato> IGeneryService<Asignar_Candidato>.GetByIdAsync(int id)
        {
            try
            {
                if (id <= 0)
                {
                    throw new ArgumentException("El ID debe ser mayor que cero.", nameof(id));
                }
                return _service.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assignment with ID {id}", ex);
            }
        }
    }

}
