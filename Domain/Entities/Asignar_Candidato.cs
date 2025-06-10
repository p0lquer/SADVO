using SADVO.Domain.Enumns;

namespace SADVO.Domain.Entities
{
  public class Asignar_Candidato
    {
        public int Id { get; set; }
        public int CandidatoId { get; set; }
        public int PuestoElectivoId { get; set; }
        public int PartidoPoliticoId { get; set; }

        public required Candidato Candidato { get; set; } 

        public required Puesto_Electivo _Electivo { get; set; } 
        public required TypeCandidate Tipo_Candidato { get; set; }
    }
}
