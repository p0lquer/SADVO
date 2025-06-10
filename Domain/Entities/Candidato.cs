using SADVO.Domain.Entities.Common.BaseEntity;
using SADVO.Domain.Enumns;

namespace SADVO.Domain.Entities
{
    public class Candidato : CommonEntity<int>
    {
        public required string Apellido { get; set; }
     

        public int? PuestoElectivoId { get; set; }
        public int? PartidoId { get; set; }
        public required Puesto_Electivo Puesto_Electivo { get; set; } 
        public required string Foto { get; set; }
        public required Puesto_Electivo PuestoElectivo { get; set; } //fk
        public required Partido_Politico Partido { get; set; } //fk
        public ICollection<Asignar_Candidato>? Asignar_Candidato { get; set; }

    }
   
}
