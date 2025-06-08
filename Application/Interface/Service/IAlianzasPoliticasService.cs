using SADVO.Domain.Entities;
namespace SADVO.Application.Interface.Service
{
    public interface IAlianzasPoliticasServicen : IGeneryService<Alianzas_Politicas>
    {
        Task<IEnumerable<Alianzas_Politicas>> GetAlianzasByPartidoAsync(int partidoId);
        Task<bool> CrearAlianzaAsync(Alianzas_Politicas alianza);
        Task<bool> ValidarAlianzaExistenteAsync(int partidoId);

    }
}
