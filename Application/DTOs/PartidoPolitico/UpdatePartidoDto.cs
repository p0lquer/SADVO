

using SADVO.Application.Common;

namespace SADVO.Application.DTOs.PartidoPolitico
{
   public class UpdatePartidoDto : BaseDto<int>

    {
        public required string Siglas { get; set; }
        public required string Description { get; set; }
        public required string Logo { get; set; }

    }
}
