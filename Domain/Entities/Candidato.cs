using SADVO.Domain.Entities.Common.BaseEntity;
using SADVO.Domain.Enumns;

namespace SADVO.Domain.Entities
{
    public class Candidato : CommonEntity<int>
    {
        public required string Apellido { get; set; }
     

        public required string Foto { get; set; }
        public required Partido_Politico Partido { get; set; }
        public ICollection<Asignar_Candidato>? Asignar_Candidato { get; set; }
    }
   
}
