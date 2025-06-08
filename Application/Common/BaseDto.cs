namespace SADVO.Application.Common
{
    public class BaseDto<TId> where TId : struct
    {
        public TId Id { get; set; }
        public required string Nombre { get; set; }
        public required bool EsActivo { get; set; }
    }
}