

using SADVO.Domain.Enumns;

namespace SADVO.Application.ViewModels.AlianzaPoliticaVM
{
    public class CreateAlianzaPoliticaViewModel : BasicViewModel<int>
    {
        public required string Descripcion { get; set; }
        public required string Siglas { get; set; }
        public required string Logo { get; set; }

        public DateTime FechaSolicitud { get; set; }
        public DateTime? FechaRespuesta { get; set; }
        public EstadoAlianza Estado { get; set; } = EstadoAlianza.Pendiente;
    }
}
