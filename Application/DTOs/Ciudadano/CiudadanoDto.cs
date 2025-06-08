

using SADVO.Application.Common;

namespace SADVO.Application.DTOs.Ciudadano
{
    public class CiudadanoDto : BaseDto<int>
    {
        public required string Apellido { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required string Email { get; set; }
    
    }
 
}
