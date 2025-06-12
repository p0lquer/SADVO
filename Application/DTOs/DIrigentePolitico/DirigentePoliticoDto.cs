
namespace SADVO.Application.DTOs.AsignacionDirigente
{

    public class DirigentePoliticoDto
    {
        public int UsuarioId { get; internal set; }
        public int PartidoPoliticoId { get; internal set; }
        public DirigentePoliticoDto? PartidoPolitico { get; internal set; }
    }

}