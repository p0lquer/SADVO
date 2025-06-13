

using SADVO.Domain.Entities;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Domain.Interface.Repository
{
    public interface IAlianzasPoliticasRepository : IGeneryRepository<Alianzas_Politica>
    {
        Task<IEnumerable<Alianzas_Politica>> GetAlianzasByPartidoAsync(int partidoId);

        Task<bool> ExisteAlianzaAsync(int partidoId);
        Task<Ciudadano?> GetByNumeroIdentificacionAsync(string numeroIdentificacion);
        Task GetByEmailAsync(string email);
        Task<IEnumerable<Dirigente_Politico>> GetDirigentesByPartidoAsync(int partidoPoliticoId);
        Task GetDirigentesByUsuarioAsync(int usuarioId);
        Task<IEnumerable<Eleccion>> GetEleccionesByFechaRangoAsync(DateTime fechaInicio, DateTime fechaFin);
        Task<IEnumerable<Eleccion>> GetEleccionesByPartidoAsync(int partidoId);
        Task<bool> ExisteDescripcionAsync(string descripcion);
        Task<Usuarios?> GetUsuarioByEmailAsync(string email);
        Task<Usuarios?> GetUsuarioConRolesAsync(int usuarioId);
        Task<bool> ExisteEmailAsync(string email);
    }
}
