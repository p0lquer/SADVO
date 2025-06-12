using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Application.DTOs.Usuarios
{
    // DTO adicional para cambio de contraseña
    public class ChangePasswordDto
    {
        public int UsuarioId { get; set; }
        public required string PasswordActual { get; set; }
        public required string PasswordNuevo { get; set; }
        public required string ConfirmarPasswordNuevo { get; set; }
    }
}
