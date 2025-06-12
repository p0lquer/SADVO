

namespace SADVO.Application.DTOs.Votos
{
    public class VotosDto
    {
        public int Id { get; set; }

        public int CiudadanoId { get; set; }
        public required string CiudadanoNombre { get; set; }

        public int EleccionId { get; set; }
        public required string EleccionNombre { get; set; }
      
        public DateTime FechaVoto { get; set; } = DateTime.UtcNow;
    }
}
