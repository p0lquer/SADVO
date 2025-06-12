

using SADVO.Application.Common;
using System.ComponentModel.DataAnnotations;

namespace SADVO.Application.DTOs.Eleccion
{
    public class UpdateEleccionDto : BaseDto<int>
    {
        [Required(ErrorMessage = "La fecha de ocurrencia es requerida")]
        [Display(Name = "Fecha de Ocurrencia")]
        public DateTime FechaOcurrida { get; set; }

        [Required(ErrorMessage = "El partido político es requerido")]
        [Display(Name = "Partido Político")]
        public int PartidoPoliticoId { get; set; }

        [Required(ErrorMessage = "Al menos un puesto electivo es requerido")]
        [Display(Name = "Puestos Electivos")]
        public List<int> PuestosElectivosIds { get; set; } = new List<int>();

        [Required(ErrorMessage = "El tipo de candidato es requerido")]
        [Display(Name = "Tipo de Candidato")]
        public Domain.Enumns.TypeCandidate TypeCandidate { get; set; }
    }
}
