

using SADVO.Domain.Enumns;

namespace SADVO.Application.DTOs.AsignacionDirigente
{
    public class AsignacionCandidatoDto
    {
        public int CandidatoId { get; set; }
        public int PuestoElectivoId { get; set; }

        public int PartidoPoliticoId { get; set; }
        public TypeCandidate TipoCandidato { get; set; }
    }
}
