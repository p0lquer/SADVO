
using SADVO.Application.Common;
using SADVO.Domain.Enumns;

namespace SADVO.Application.DTOs.Candidato
{
    public class CreateCandidatoDto : BaseDto<int>
    {
        public required int PartidoId { get; set; }
        public required string Apellido { get; set; }
        public required string nombre { get; set; }

        public required TypeCandidate TipoCandidato { get; set; }
        public required int PuestoElectivoId { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required string Email { get; set; }
        public required string Telefono { get; set; }
        public required string Direccion { get; set; }
        public required string FechaNacimiento { get; set; }
        public required string Genero { get; set; }
        public required string EstadoCivil { get; set; }
        public required string Nacionalidad { get; set; }
        public required string Ocupacion { get; set; }

        public required string Foto { get; set; }
    }
}
