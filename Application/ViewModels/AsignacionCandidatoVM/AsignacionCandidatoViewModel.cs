

using SADVO.Domain.Entities;

namespace SADVO.Application.ViewModels.AsignacionCandidatoVM
{
    public class AsignacionCandidatoViewModel
    {
        public int Id { get; set; }
        public int CandidatoId { get; set; }
        public int PuestoElectivoId { get; set; }
        public int PartidoPoliticoId { get; set; }

        public required Candidato Candidato { get; set; }
        public required Puesto_Electivo _Electivo { get; set; }
        public required Partido_Politico PartidoPolitico { get; set; }
    }
}
