

using System.ComponentModel.DataAnnotations;
using SADVO.Application.DTOs.Usuarios;

namespace SADVO.Application.DTOs.Usuarios
{
   
    public class LoginViewModel
    {
        [Required(ErrorMessage ="Debe ingresar el nombre del usuario")]
        [DataType(DataType.Text)]
        public required string UserName { get; set; }

        [Required(ErrorMessage = "DEbe ingresar la contraseña")]
        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }

}
