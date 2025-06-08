using SADVO.Domain.Entities.Common.BaseEntity;

namespace SADVO.Domain.Entities
{
    public class Ciudadano : CommonEntity<int>
    {
        public required string Apellido { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required string Email { get; set; }

        public required bool EsActivo { get; set; } = true;
    }
}