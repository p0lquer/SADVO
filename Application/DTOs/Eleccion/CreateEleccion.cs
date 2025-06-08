

using SADVO.Application.Common;

namespace SADVO.Application.DTOs.Eleccion
{
   public class CreateEleccion : BaseDto<int>
    {
      
        public DateTime FechaOcurrida { get; set; }
      
    }
}
