

using SADVO.Domain.Enumns;

namespace SADVO.Application.ViewModels.Usuario
{
    public class UserViewModel : BasicViewModel<int>

    {
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmationPassword { get; set; }

        public string? Telefono { get; set; }
        public string? Foto { get; set; }
        public RolUsuario Role { get; set; }
    }
}
