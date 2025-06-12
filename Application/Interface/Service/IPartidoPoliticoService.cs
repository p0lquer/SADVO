using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface IPartidoPoliticoService : IGeneryService<Partido_Politico>
    {
        Task<IEnumerable<Partido_Politico>> GetPartidosActivosAsync();
        Task<bool> ActivarDesactivarPartidoAsync(int partidoId, bool estado);
        Task<bool> ActualizarLogoPartidoAsync(int partidoId, string nuevoLogo);
        Task<bool> ValidarSiglasUnicasAsync(string siglas);
        Task<Partido_Politico?> GetPartidoConDetallesAsync(int partidoId);
        Task<Partido_Politico?> GetBySiglasAsync(string siglas);
    }
}