
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace SADVO.Application.ViewModels.User
{
    public class SaveUserViewModel : BasicViewModel<int>
    {
        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [DataType(DataType.Text)]
        public required string Apellido { get; set; }
        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [DataType(DataType.Text)]
        public required string Email { get; set; }
        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }

        [Compare(nameof(Password), ErrorMessage = "Las contraseñas no coinciden")]
        [Required(ErrorMessage = "Este Campo es obligatorio")]
        [DataType(DataType.Password)]
        public required string ConfirmationPassword { get; set; }

        [DataType(DataType.Text)]
        public string? Telefono { get; set; }
        public string? Foto { get; set; }

        [Range (1, int.MaxValue, ErrorMessage ="Coloca un valor valido para el rol del usuario")]
        public required int Role { get; set; }
    }
}
