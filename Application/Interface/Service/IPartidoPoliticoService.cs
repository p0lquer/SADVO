using SADVO.Application.DTOs.PartidoPolitico;
using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface IPartidoPoliticoService : IGeneryService<Partido_Politico>
    {
        Task<IEnumerable<PartidoDto>> GetPartidosActivosAsync();
        Task<bool> ActivarDesactivarPartidoAsync(int partidoId, bool estado);
        Task<bool> ActualizarLogoPartidoAsync(int partidoId, string nuevoLogo);
        Task<bool> ValidarSiglasUnicasAsync(string siglas);
        Task<PartidoDto> GetPartidoConDetallesAsync(int partidoId);
        Task<IEnumerable<PartidoDto>> GetBySiglasAsync(string siglas);
    }
}