
using SADVO.Domain.Entities.Common.BaseEntity;

namespace SADVO.Domain.Entities
{
  public class Puesto_Electivo : CommonEntity<int>
    {

        public required bool EsActivo { get; set; } = true;
        public required string Description { get; set; }

        public ICollection<Eleccion>? Eleccion { get; set; }
    }
}
