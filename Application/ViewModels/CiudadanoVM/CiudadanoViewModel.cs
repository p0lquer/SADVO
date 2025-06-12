

namespace SADVO.Application.ViewModels.CiudadanoVM
{
    public class CiudadanoViewModel : BasicViewModel<int>
    {
        public required string Apellido { get; set; }
        public required string NumeroIdentificacion { get; set; }
        public required string Email { get; set; }
    }
}

