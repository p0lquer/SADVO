

using SADVO.Domain.Entities.Common.BaseEntity;

namespace SADVO.Domain.Entities
{
    public class Usuarios : CommonEntity<int>
    {
        public required string Apellido { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; } 
        public required string ConfirmationPassword { get; set; }

        public  ICollection<Dirigente_Politico>? DirigentePoliticos { get; set; }
    }
}
