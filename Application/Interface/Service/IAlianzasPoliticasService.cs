using SADVO.Domain.Entities;
namespace SADVO.Application.Interface.Service
{
    public interface IAlianzasPoliticasServicen : IGeneryService<Alianzas_Politica>
    {
        Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId);
        Task<bool> CrearAlianzaAsync(Alianzas_Politica alianza);
        Task<bool> ValidarAlianzaExistenteAsync(int partidoId);

    }
}
