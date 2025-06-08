

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

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

        public Task<bool> ActivarDesactivarPuestoAsync(int puestoId, bool estado)
        {
            throw new NotImplementedException();
        }

        public Task<Puesto_Electivo?> GetPuestoConHistorialAsync(int puestoId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Puesto_Electivo>> GetPuestosActivosAsync()
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarDescripcionUnicaAsync(string descripcion)
        {
            throw new NotImplementedException();
        }
    }
}
