using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Application.DTOs.DIrigentePolitico
{
    public class DirigentePoliticoDetailsDto
    {
        public int Id { get; set; }

        [Display(Name = "Usuario")]
        public int UsuarioId { get; set; }

        [Display(Name = "Partido Político")]
        public int PartidoPoliticoId { get; set; }

        // Información detallada del usuario
        [Display(Name = "Nombre del Usuario")]
        public string? UsuarioNombre { get; set; }

        [Display(Name = "Email del Usuario")]
        public string? UsuarioEmail { get; set; }

        // Información detallada del partido
        [Display(Name = "Nombre del Partido")]
        public string? PartidoPoliticoNombre { get; set; }

        [Display(Name = "Descripción del Partido")]
        public string? PartidoPoliticoDescripcion { get; set; }

        [Display(Name = "Fecha de Asignación")]
        public DateTime? FechaAsignacion { get; set; }
    }
}
