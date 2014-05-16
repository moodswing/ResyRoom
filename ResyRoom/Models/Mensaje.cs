using ResyRoom.ViewModels;

namespace ResyRoom.Models
{
    public class Mensaje : BaseViewModel
    {
        public string TituloPagina { get; set; }
        public string TituloMensaje { get; set; }
        public string Texto { get; set; }
    }
}