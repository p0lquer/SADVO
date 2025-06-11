

using SADVO.Application.DTOs.Usuarios;

namespace SADVO.Application.DTOs.Usuarios
{
   
    public class LoginDto
    {
        public required string UserName { get; set; }
        public required string Password { get; set; }
    }

}
