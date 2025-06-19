using SADVO.Domain.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;
using SADVO.Interfaces.Interface.Repository;
using SADVO.Application.ViewModels.CandidatoVM;
using SADVO.Application.DTOs.Candidato;

namespace SADVO.Application.Service
{
    public class CandidatoService : GeneryService<Candidato>, ICandidatoService
    {
        private readonly ICandidatoRepository _candidatoRepository;

        public CandidatoService(ICandidatoRepository candidatoRepository)
            : base(candidatoRepository)
        {
            _candidatoRepository = candidatoRepository;
        }


        public async Task<CandidatoDto> CreateAsync(CandidatoDto vm)
        {

            var entity = new Candidato
            {
                Apellido = vm.Apellido,

                Foto = vm.Foto,
                Nombre = vm.Nombre,
                EsActivo = vm.EsActivo,
                PuestoElectivo = vm.PuestoElectivo,
                Partido = vm.Partido
            };

           var servicio = await _candidatoRepository.AddAsync(entity);
            if (servicio == null)
            {
                throw new Exception("Error al crear el candidato.");
            }
            return new CandidatoDto
            {
                Id = servicio.Id,
                Nombre = servicio.Nombre,
                Apellido = servicio.Apellido,
                Foto = servicio.Foto,
                EsActivo = servicio.EsActivo,
                PuestoElectivo = servicio.PuestoElectivo,
                Partido = servicio.Partido,
                    PuestoElectivoId = servicio.PuestoElectivoId ?? 0, // Ensure required property is set
                PartidoId = servicio.PartidoId ?? 0,
            };

        }


        public async Task<bool> ActivarDesactivarCandidatoAsync(int candidatoId, bool estado)
        {
            try
            {
                if (candidatoId <= 0)
                {
                    throw new ArgumentException("El ID del candidato debe ser mayor que cero.", nameof(candidatoId));
                }
                var candidato = await _candidatoRepository.GetByIdAsync(candidatoId);
                if (candidato == null)
                {
                    throw new KeyNotFoundException($"Candidato con ID {candidatoId} no encontrado.");
                }
                candidato.EsActivo = true;
                await _candidatoRepository.UpdateAsync(candidato);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al activar/desactivar el candidato con ID {candidatoId}", ex);

            }
        }

        public async Task<bool> ActualizarFotoCandidatoAsync(int candidatoId, string nuevaFoto)
        {
            try
            {
                if (candidatoId <= 0)
                {
                    throw new ArgumentException("El ID del candidato debe ser mayor que cero.", nameof(candidatoId));
                }
                var candidato = await _candidatoRepository.GetByIdAsync(candidatoId);
                if (candidato == null)
                {
                    throw new KeyNotFoundException($"Candidato con ID {candidatoId} no encontrado.");
                }
                candidato.EsActivo  =true;
                await _candidatoRepository.UpdateAsync(candidato);
                return true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error al actualizar la foto del candidato con ID {candidatoId}", ex);
            }
        }

        public async Task<IEnumerable<Candidato>> GetCandidatosActivosAsync()
        {
            try
            {
                var candidatos = await _candidatoRepository.GetAllAsync();
                if (candidatos == null || !candidatos.Any())
                {
                    return Enumerable.Empty<Candidato>();
                }
                return candidatos.Where(c => c.EsActivo == true).Select(candidatos => new Candidato
                {
                    Id = candidatos.Id,
                    Nombre = candidatos.Nombre, 
                    Apellido = candidatos.Apellido,
                    Foto = candidatos.Foto,
                    EsActivo = candidatos.EsActivo,
                    Partido = candidatos.Partido,
                    PuestoElectivo = candidatos.PuestoElectivo,
                    TypeCandidate = candidatos.TypeCandidate

                }).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving active candidates", ex);
            }
        }


        public async Task<IEnumerable<CandidatoDto>> GetCandidatosByPartidoAsync(int partidoId)
        {
            try
            {
                if (partidoId <= 0)
                {
                    throw new ArgumentException("El ID del partido debe ser mayor que cero.", nameof(partidoId));
                }
                var candidatos = await _candidatoRepository.GetAllAsync();
                return (IEnumerable<CandidatoDto>) candidatos.Where(c => c.Equals(partidoId) && c.Partido.EsActivo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error retrieving candidates for party with ID {partidoId}", ex);

            }
        }

        public async Task<bool> ValidarCandidatoUnicoAsync(string apellido)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(apellido) || apellido.Length > 12)
                {
                    throw new ArgumentException("El apellido no puede ser null o vacío y debe tener un máximo de 12 caracteres.", nameof(apellido));
                }
               
                var candidatos = await _candidatoRepository.GetAllAsync();
                return !candidatos.Any(c => c.Apellido.Equals(apellido, StringComparison.OrdinalIgnoreCase));
            }
            catch (Exception ex)
            {
                throw new Exception($"Error validating unique candidate by last name {apellido}", ex);
            }
        }

        
        public async Task<CandidatoDto> UpdateAsync(CandidatoDto dto, int id)
        {
            var entity = await _candidatoRepository.GetByIdAsync(dto.Id);
            if (entity == null) 
                throw new Exception("Error");

            entity.Apellido = dto.Apellido;
            entity.Foto = dto.Foto;
            // ...other updates

            await _candidatoRepository.UpdateAsync(entity);
            return
                new CandidatoDto
                {
                    Id = entity.Id,
                    Nombre = entity.Nombre,
                    EsActivo = entity.EsActivo,
                    Apellido = entity.Apellido,
                    Foto = entity.Foto,
                    PuestoElectivoId = entity.PuestoElectivoId ?? 0, // Ensure this required property is set
                    PartidoId = entity.PartidoId ?? 0
                };
        }

        Task<IEnumerable<CandidatoDto>> ICandidatoService.GetCandidatosActivosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
