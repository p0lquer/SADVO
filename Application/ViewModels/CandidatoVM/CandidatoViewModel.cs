


using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.ViewModels.CandidatoVM
{
    public class CandidatoViewModel : BasicViewModel<int>
    {

        public string Apellido { get; set; } = string.Empty;

        public TypeCandidate typeCandidate { get; set; }
        public int? PuestoElectivoId { get; set; }
        public int? PartidoId { get; set; }
  
        public required string Foto { get; set; }
        public Puesto_Electivo? PuestoElectivo { get; set; }
        public  Partido_Politico? Partido { get; set; } 
        public ICollection<Asignar_Candidato>? Asignar_Candidato { get; set; }
    }
}
