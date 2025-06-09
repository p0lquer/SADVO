

using SADVO.Domain.Entities.Common.BaseEntity;

namespace SADVO.Domain.Entities
{
   public class Eleccion : CommonEntity<int>
    {
        

        public required DateTime FechaOcurrida { get; set; }

        public  required Partido_Politico PartidoPolitico { get; set; }  

        public required ICollection<Puesto_Electivo> PuestoElectivo { get; set; }
   }
}
