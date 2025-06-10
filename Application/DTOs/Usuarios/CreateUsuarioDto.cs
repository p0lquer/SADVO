

using SADVO.Application.Common;

namespace SADVO.Application.DTOs.Usuarios
{
    public class CreateUsuarioDto : BaseDto<int>
    {
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmationPassword { get; set; }

        public string? Telefono { get; set; }
        public string? Foto { get; set; }
        public required int Role { get; set; }
    }
}
