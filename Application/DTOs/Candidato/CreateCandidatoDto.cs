
using SADVO.Application.Common;

namespace SADVO.Application.DTOs.Candidato
{
    public class CreateCandidatoDto : BaseDto<int>
    {
        public required string Apellido { get; set; }
   
        public required string Foto { get; set; }
    }
}
