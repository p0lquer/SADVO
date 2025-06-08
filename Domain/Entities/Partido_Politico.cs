using SADVO.Domain.Entities.Common.BaseEntity;

namespace SADVO.Domain.Entities
{
    public class Partido_Politico : CommonEntity<int>
    {
     

        public required string Siglas { get; set; }
        public required string Description { get; set; }

        public required string Logo { get; set; }

        public  Solicitudes? Solicitudes { get; set; }
        public ICollection<Dirigente_Politico>? DirigentePoliticos { get; set; } 
    }
}
