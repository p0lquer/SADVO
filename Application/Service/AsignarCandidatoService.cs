using SADVO.Application.DTOs.AsignacionDirigente;
using SADVO.Application.DTOs.Candidato;
using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Application.Service
{
    public class AsignarCandidatoService : GeneryService<Asignar_Candidato>, IAsignarCandidatoService
    {
        private readonly IAsignarCandidatoRepository _asignarCandidatoRepository;
        public AsignarCandidatoService(IAsignarCandidatoRepository service) : base(service)
        {

            _asignarCandidatoRepository = service ?? throw new ArgumentNullException(nameof(service));
        }

        public async Task<bool> AsignarCandidatoAPuestoAsync(int candidatoId, int puestoElectivoId, TypeCandidate tipoCandidato, int partidoPoliticoId)
        {
            try
            {
                if (candidatoId <= 0 || puestoElectivoId <= 0)
                {
                    throw new ArgumentException("Los IDs de candidato y puesto electivo deben ser mayores que cero.");
                }
                if (await _asignarCandidatoRepository.ExisteAsignacionAsync(candidatoId, puestoElectivoId))
                {
                    throw new InvalidOperationException("El candidato ya está asignado a este puesto electivo con el mismo tipo de candidato.");
                }

                var asignacion = new Asignar_Candidato
                {
                    CandidatoId = candidatoId,
                    PuestoElectivoId = puestoElectivoId,
                    Tipo_Candidato = tipoCandidato,
                    PartidoPoliticoId = partidoPoliticoId,
                    
                };
                await _asignarCandidatoRepository.AddAsync(asignacion);
                return true;

            }
            catch (Exception ex)
            {
                throw new Exception($"Error al asignar candidato con ID {candidatoId} al puesto con ID {puestoElectivoId}", ex);
            }
        }

        public Task<bool> AsignarCandidatoAPuestoAsync(int candidatoId, int puestoElectivoId, TypeCandidate tipoCandidato)
        {
            throw new NotImplementedException();
        }

        public override async Task<Asignar_Candidato> CreateAsync(Asignar_Candidato entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                

                if(await _asignarCandidatoRepository.ExisteAsignacionAsync(entity.CandidatoId, entity.PuestoElectivoId))
                {
                    throw new InvalidOperationException("El candidato ya está asignado a este puesto electivo.");
                }
                return await _asignarCandidatoRepository.AddAsync(entity);
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
                    TipoCandidato = asignacion.TipoCandidato,
                    PartidoPoliticoId = asignacion.PartidoPoliticoId
                };

                await CreateAsync(entity);
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
                return await _asignarCandidatoRepository.GetAsignacionesByCandidatoAsync(candidatoId);
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

                var asignacion = await _asignarCandidatoRepository.GetAsignacionesByCandidatoAsync(candidatoId);
               var asignacionToRemove = asignacion.FirstOrDefault(a => a.PuestoElectivoId == puestoElectivoId);
                if (asignacionToRemove == null)
                {
                    throw new InvalidOperationException("La asignación no existe.");
                }
             return await _asignarCandidatoRepository.DeleteAsync(asignacionToRemove.Id);
            
            }
            catch (Exception ex)
            {
                throw new Exception($"Error removing assignment for candidate ID {candidatoId} and position ID {puestoElectivoId}", ex);
            }
        }

        public override async Task<Asignar_Candidato> UpdateAsync(Asignar_Candidato entity)
        {
            try
            {
                if (entity == null)
                    throw new ArgumentNullException(nameof(entity), "Entity cannot be null.");
                return await _asignarCandidatoRepository.UpdateAsync(entity);
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
                return await _asignarCandidatoRepository.ExisteAsignacionAsync(candidatoId, puestoElectivoId);
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
                return _asignarCandidatoRepository.GetAllAsync();
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
                return _asignarCandidatoRepository.GetByIdAsync(id);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving assignment with ID {id}", ex);
            }
        }
    }

}
