

namespace SADVO.Application.DTOs.Common
{
    public class BaseDto<TId> where TId : struct
    {
        public TId Id { get; set; }
        public required string Nombre { get; set; }
    }
}