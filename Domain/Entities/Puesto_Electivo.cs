
using SADVO.Domain.Entities.Common.BaseEntity;

namespace SADVO.Domain.Entities
{
  public class Puesto_Electivo : CommonEntity<int>
    {

     
        public required string Description { get; set; }

        public ICollection<Candidato>? Candidatos { get; set; } //fk
        public ICollection<Eleccion>? Eleccion { get; set; }
    }
}
