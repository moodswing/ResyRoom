using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using ResyRoom.Models;
using Estudio = ResyRoom.Models.Estudio;

namespace ResyRoom.ViewModels
{
    public class RegisterStudioViewModel : BaseViewModel, IRegisterStudioViewModel
    {
        public Estudio Estudio { get; set; }
        public RegistroDeUsuario Usuario { get; set; }

        public IList<Region> Regiones { get; set; }
        public IList<Comuna> Comunas { get; set; }
    }

    public interface IRegisterStudioViewModel
    {
        Estudio Estudio { get; set; }
        List<Notificacion> Notificaciones { get; set; }
        bool MostrarNotificacion { get; set; }
        RegistroDeUsuario Usuario { get; set; }

        string TituloMensaje { get; set; }
        string SubtituloMensaje { get; set; }
        IList<Region> Regiones { get; set; }
        IList<Comuna> Comunas { get; set; }
    }
}