

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.Service
{
    public class AsignarCandidatoService : GeneryService<AsignarCandidatoService>, IAsignarCandidatoService
    {
        private readonly IAsignarCandidatoService _service;
        public AsignarCandidatoService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {

            _service = (IAsignarCandidatoService?)(alianzasPoliticasRepository ?? throw new ArgumentNullException(nameof(alianzasPoliticasRepository))); 
        }

        public Task<bool> AsignarCandidatoAPuestoAsync(int candidatoId, int puestoElectivoId, TypeCandidate tipoCandidato)
        {
            throw new NotImplementedException();
        }

        public Task<Asignar_Candidato> CreateAsync(Asignar_Candidato entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Asignar_Candidato>> GetAsignacionesByCandidatoAsync(int candidatoId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RemoverAsignacionAsync(int candidatoId, int puestoElectivoId)
        {
            throw new NotImplementedException();
        }

        public Task<Asignar_Candidato> UpdateAsync(Asignar_Candidato entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarAsignacionDuplicadaAsync(int candidatoId, int puestoElectivoId)
        {
            throw new NotImplementedException();
        }

        Task<IEnumerable<Asignar_Candidato>> IGeneryService<Asignar_Candidato>.GetAllAsync()
        {
            throw new NotImplementedException();
        }

        Task<Asignar_Candidato> IGeneryService<Asignar_Candidato>.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }
    }

}
