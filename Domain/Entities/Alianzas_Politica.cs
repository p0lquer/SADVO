




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


        public Partido_Politico PartidoSolicitante { get; set; } = null!;
        public Partido_Politico PartidoReceptor { get; set; } = null!;
        public string Apellido { get; set; } = null!;
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

    }

}