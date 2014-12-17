using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ResyRoom.Infraestructura.Validators;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class RegisterStudioViewModel : BaseViewModel, IRegisterStudioViewModel
    {
        public StudioViewModel Estudio { get; set; }
        public RegistroDeUsuario Usuario { get; set; }
        
        public int PasoNumero { get; set; }
        public int PasoActual { get; set; }
        
        public IList<Region> Regiones { get; set; }
        public IList<Comuna> Comunas { get; set; }
        public IList<TipoEquipo> TipoEquipos { get; set; }

        public RegisterStudioViewModel()
        {
            Estudio = new StudioViewModel
                {
                    IdRegion = 46
                };
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
        int PasoActual { get; set; }
        string JsonModelResult { get; set; }

        string TituloMensaje { get; set; }
        string SubtituloMensaje { get; set; }
        IList<Region> Regiones { get; set; }
        IList<Comuna> Comunas { get; set; }
    }
}