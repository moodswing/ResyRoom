using System.Collections.Generic;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class RegisterStudioViewModel : BaseViewModel, IRegisterStudioViewModel
    {
        public StudioViewModel Estudio { get; set; }
        public RegistroDeUsuario Usuario { get; set; }

        public int PasoNumero { get; set; }
        public int EliminaSalaIndice { get; set; }

        public IList<Region> Regiones { get; set; }
        public IList<Comuna> Comunas { get; set; }

        public RegisterStudioViewModel()
        {
            Estudio = new StudioViewModel();
            Usuario = new RegistroDeUsuario();
        }
    }

    public interface IRegisterStudioViewModel
    {
        StudioViewModel Estudio { get; set; }
        List<Notificacion> Notificaciones { get; set; }
        bool MostrarNotificacion { get; set; }
        RegistroDeUsuario Usuario { get; set; }

        int PasoNumero { get; set; }
        int EliminaSalaIndice { get; set; }

        string TituloMensaje { get; set; }
        string SubtituloMensaje { get; set; }
        IList<Region> Regiones { get; set; }
        IList<Comuna> Comunas { get; set; }
    }
}