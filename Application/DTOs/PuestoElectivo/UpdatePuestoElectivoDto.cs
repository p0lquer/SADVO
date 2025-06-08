
using SADVO.Application.Common;

namespace SADVO.Application.DTOs.PuestoElectivo
{
   public class UpdatePuestoElectivoDto :  BaseDto<int>
    {
        public required string Description { get; set; }
    }
}
