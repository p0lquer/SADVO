

using SADVO.Application.Common;
using SADVO.Application.DTOs.PartidoPolitico;
using SADVO.Application.DTOs.PuestoElectivo;
using SADVO.Application.DTOs.Votos;

namespace SADVO.Application.DTOs.Eleccion
{
    public class EleccionDto : BaseDto<int>
    {
        public DateTime FechaOcurrida { get; set; }
        public PartidoDto PartidoPolitico { get; set; }
        public List<PuestoElectivoDto> PuestosElectivos { get; set; } = new List<PuestoElectivoDto>();
        public List<VotosDto> Votos { get; set; } = new List<VotosDto>();
        public Domain.Enumns.TypeCandidate TypeCandidate { get; set; }
        public string TypeCandidateDisplay => TypeCandidate.ToString();
    }
}
