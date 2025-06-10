
using System.ComponentModel.DataAnnotations;

namespace SADVO.Application.ViewModels
{
    public class BasicViewModel<TId> where TId : struct
    {
        public TId Id { get; set; }
        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [DataType(DataType.Text)]
        public required string Nombre { get; set; }
        [Required(ErrorMessage = "Este Campo es obligatorio")]
        public required bool EsActivo { get; set; }
    }
}
