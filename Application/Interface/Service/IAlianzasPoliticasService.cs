using SADVO.Domain.Entities;
namespace SADVO.Application.Interface.Service
{
    public interface IAlianzasPoliticasService : IGeneryService<Alianzas_Politica>
    {
        Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId);
        Task<bool> CrearAlianzaAsync(Alianzas_Politica alianza);
        Task<bool> ValidarAlianzaExistenteAsync(int partidoId);

        Task<Alianzas_Politica> CrearSolicitudAlianza(int partidoSolicitanteId, int partidoReceptorId);
        Task<Alianzas_Politica>ResponderAlianza(int alianzaId, bool aceptar);
    }
}
