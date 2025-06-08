using SADVO.Domain.Enumns;

namespace SADVO.Domain.Entities
{
  public class Asignar_Candidato
    {
        public required Candidato Candidato { get; set; } 

        public required Puesto_Electivo _Electivo { get; set; } 
        public required TypeCandidate Tipo_Candidato { get; set; }
    }
}
