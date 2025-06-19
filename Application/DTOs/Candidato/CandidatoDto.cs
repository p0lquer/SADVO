

using SADVO.Application.Common;
using SADVO.Domain.Entities;
using SADVO.Domain.Enumns;

namespace SADVO.Application.DTOs.Candidato
{
    public class CandidatoDto : BaseDto<int>
    {
        public required string Apellido { get; set; }

        public required int PuestoElectivoId { get; set; }

        public required int PartidoId { get; set; }
        public required string Foto { get; set; }

        public Partido_Politico? Partido { get; set; }

        public Puesto_Electivo? PuestoElectivo { get; set; }

        public TypeCandidate TypeCandidate { get; set; }
    }
    }
