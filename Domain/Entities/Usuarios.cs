

using SADVO.Domain.Entities.Common.BaseEntity;
using SADVO.Domain.Enumns;

namespace SADVO.Domain.Entities
{
    public class Usuarios : CommonEntity<int>
    {
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } 
        public required string ConfirmationPassword { get; set; }
        public required RolUsuario RolUsuario { get; set; }

        public string? Telefono { get; set; }

        public string? Foto { get; set; }

        public RolUsuario Role { get; set; } 
        public  ICollection<Dirigente_Politico>? DirigentePoliticos { get; set; }
    }
}
