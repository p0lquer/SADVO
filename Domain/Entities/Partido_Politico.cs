using System.ComponentModel.DataAnnotations.Schema;
using SADVO.Domain.Entities.Common.BaseEntity;
using SADVO.Domain.Enumns;


namespace SADVO.Domain.Entities
{
    public class Partido_Politico : CommonEntity<int>
    {
     

        public required string Siglas { get; set; }
        public required string Description { get; set; }
        public required string Logo { get; set; }


        // Propiedades de navegación para alianzas
        public ICollection<Alianzas_Politica> AlianzasSolicitadas { get; set; } = new List<Alianzas_Politica>();
        public ICollection<Alianzas_Politica> AlianzasRecibidas { get; set; } = new List<Alianzas_Politica>();


        [NotMapped]
        public IEnumerable<Alianzas_Politica> AlianzasActivas =>
            AlianzasSolicitadas.Concat(AlianzasRecibidas)
                .Where(a => a.Estado == EstadoAlianza.Aceptada);

        [NotMapped]
        public IEnumerable<Alianzas_Politica> SolicitudesPendientes =>
            AlianzasRecibidas.Where(a => a.Estado == EstadoAlianza.Pendiente);



        public ICollection<Candidato>? Candidatos { get; set; }
        public ICollection<List<Dirigente_Politico>>? DirigentePoliticos { get; set; }

    
    }
}
