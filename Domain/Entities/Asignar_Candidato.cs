

namespace SADVO.Domain.Entities
{
  public class Asignar_Candidato
    {
        public int Id { get; set; }
        public int CandidatoId { get; set; }
        public int PuestoElectivoId { get; set; }
        public int PartidoPoliticoId { get; set; }

        public required Candidato Candidato { get; set; } 

        public required Puesto_Electivo Puesto_Electivo { get; set; } 

        public required Partido_Politico PartidoPolitico { get; set; }
    }
}
