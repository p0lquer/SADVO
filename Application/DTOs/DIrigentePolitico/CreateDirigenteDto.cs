using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Application.DTOs.DIrigentePolitico
{
    public class CreateDirigenteDto
    {
        [Required(ErrorMessage = "El usuario es requerido")]
        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El partido político es requerido")]
        [Display(Name = "Partido Político")]
        public int PartidoPoliticoId { get; set; }
    }
}
