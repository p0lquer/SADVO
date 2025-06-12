
using SADVO.Application.ViewModels.Usuario;

namespace SADVO.Application.Interface.Service
{
    public interface IUsuariosSeccion
    {
        UserViewModel? GetUserSession();
        bool HasUser();

        bool IsAdmin();
    }
}
