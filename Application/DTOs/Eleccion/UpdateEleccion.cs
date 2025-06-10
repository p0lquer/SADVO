

using SADVO.Application.Common;

namespace SADVO.Application.DTOs.Eleccion
{
   public class UpdateEleccion : BaseDto<int>
    {
      
        public DateTime FechaOcurrida { get; set; }
      
    }
}
