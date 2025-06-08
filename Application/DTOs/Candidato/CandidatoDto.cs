

namespace SADVO.Application.DTOs.Candidato
{
    public class CandidatoDto
    {
        public required string Apellido { get; set; }
        public required bool EsActivo { get; set; } = true;

        public required string Foto { get; set; }
    }
}
