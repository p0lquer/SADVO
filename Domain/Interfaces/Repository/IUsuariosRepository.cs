

using SADVO.Domain.Entities;
using SADVO.Interfaces.Interface.Repository;

namespace SADVO.Domain.Interface.Repository
{
    public interface IUsuariosRepository : IGeneryRepository<Usuarios>
    {
        Task<Usuarios?> GetByEmailAsync(string email);

        Task<Usuarios?> GetUsuarioWithDirigentesAsync(int usuarioId);

        Task<IEnumerable<Usuarios>> GetUsuariosByApellidoAsync(string apellido);

        Task<bool> ExisteEmailAsync(string email);

        Task<bool> ValidarCredencialesAsync(string email, string password);

    }
}
