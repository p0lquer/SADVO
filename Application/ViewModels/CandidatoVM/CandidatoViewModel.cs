


using SADVO.Domain.Entities;

namespace SADVO.Application.ViewModels.CandidatoVM
{
    public class CandidatoViewModel : BasicViewModel<int>
    {

        public string Apellido { get; set; } = string.Empty;

        public int? PuestoElectivoId { get; set; }
        public int? PartidoId { get; set; }
        public required Puesto_Electivo Puesto_Electivo { get; set; }
        public required string Foto { get; set; }
        public required Puesto_Electivo PuestoElectivo { get; set; }
        public required Partido_Politico Partido { get; set; } 
        public ICollection<Asignar_Candidato>? Asignar_Candidato { get; set; }
    }
}
