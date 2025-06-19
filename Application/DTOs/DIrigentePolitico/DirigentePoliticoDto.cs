
using System.ComponentModel.DataAnnotations;

namespace SADVO.Application.DTOs.AsignacionDirigente
{

    public class DirigentePoliticoDto
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Display(Name = "Partido Político")]
        public int PartidoPoliticoId { get; set; }

     
        [Display(Name = "Usuario")]
        public string? UsuarioNombre { get; set; }

        [Display(Name = "Partido Político")]
        public string? PartidoPoliticoNombre { get; set; }
    }

}