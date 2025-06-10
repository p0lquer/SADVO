

using SADVO.Domain.Entities;

namespace SADVO.Application.Interface.Service
{
    public interface IUsuariosService : IGeneryService<Usuarios>
    {
        Task<Usuarios?> AutenticarUsuarioAsync(string email, string password);
        Task<bool> CambiarPasswordAsync(int usuarioId, string passwordActual, string passwordNuevo);
        Task<bool> ValidarEmailUnicoAsync(string email);
        Task<bool> ValidarPasswordsCoincidanAsync(string password, string confirmationPassword);
        Task<Usuarios?> GetUsuarioConRolesAsync(int usuarioId);

    }
}
