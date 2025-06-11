




using System.ComponentModel.DataAnnotations.Schema;
using SADVO.Domain.Enumns;

namespace SADVO.Domain.Entities
{
    public class Alianzas_Politica
    {
        public int Id { get; set; }
        public int PartidoSolicitanteId { get; set; }
        public int PartidoReceptorId { get; set; }
        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public EstadoAlianza Estado { get; set; } = EstadoAlianza.Pendiente;

        public required Partido_Politico PartidoSolicitante { get; set; }
        public required Partido_Politico PartidoReceptor { get; set; }
    }

}