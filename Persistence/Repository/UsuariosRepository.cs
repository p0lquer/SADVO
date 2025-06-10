using SADVO.Application.Interface.Repository;
using SADVO.Domain.Entities;
using SADVO.Persistence.Context;

namespace SADVO.Persistence.Repository
{
    public class UsuariosRepository : GeneryRepository<Usuarios>, IUsuariosRepository
    {
        private readonly SADVOContext _context;
        public UsuariosRepository(SADVOContext context) : base(context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public Task<bool> ExisteEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<Usuarios?> GetByEmailAsync(string email)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Usuarios>> GetUsuariosByApellidoAsync(string apellido)
        {
            throw new NotImplementedException();
        }

        public Task<Usuarios?> GetUsuarioWithDirigentesAsync(int usuarioId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ValidarCredencialesAsync(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}
