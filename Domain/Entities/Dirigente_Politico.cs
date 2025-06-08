
namespace SADVO.Domain.Entities
{
    public class Dirigente_Politico
    {
        public int Id { get; set; }
        public int UsuarioId { get; set; }
        public int PartidoPoliticoId { get; set; }

        // Navigation properties
        public  Usuarios? Usuario { get; set; }
        public  Partido_Politico? PartidoPolitico { get; set; }
    }
}