

using SADVO.Application.Interface.Repository;
using SADVO.Application.Interface.Service;
using SADVO.Domain.Entities;

namespace SADVO.Application.Service
{
    public class UsuariosService : GeneryService<Usuarios>, IUsuariosService
    {
        private readonly IAlianzasPoliticasRepository _alianzasPoliticasRepository;
        public UsuariosService(IAlianzasPoliticasRepository alianzasPoliticasRepository) : base(alianzasPoliticasRepository)
        {
            _alianzasPoliticasRepository = alianzasPoliticasRepository;
        }

        public Task<Usuarios?> AutenticarUsuarioAsync(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<bool> CambiarPasswordAsync(int usuarioId, string passwordActual, string passwordNuevo)
        {
            throw new NotImplementedException();
        }

        public Task<Usuarios?> GetUsuarioConRolesAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarEmailUnicoAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarPasswordsCoincidanAsync(string password, string confirmationPassword)
        {
            throw new NotImplementedException();
        }
    }
}
