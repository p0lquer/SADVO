
namespace SADVO.Application.ViewModels.PartidoPoliticoVM
{
    public class PartidoPoliticoViewModel : BasicViewModel<int>
    {
        public required string Siglas { get; set; }
        public required string Description { get; set; }
        public required string Logo { get; set; }

    }
}
