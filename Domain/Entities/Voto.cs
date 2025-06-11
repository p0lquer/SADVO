using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADVO.Domain.Entities
{
    public class Voto
    {
        public int Id { get; set; }

        //fk
        public int CiudadanoId { get; set; }
        public required Ciudadano Ciudadano { get; set; }

        //fk
        public int EleccionId { get; set; }
        public required Eleccion Eleccion { get; set; }
    }
}
