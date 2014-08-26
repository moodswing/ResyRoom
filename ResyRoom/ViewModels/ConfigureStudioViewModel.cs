using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using ResyRoom.Models;
using Estudio = ResyRoom.Models.Estudio;

namespace ResyRoom.ViewModels
{
    public class ConfigureStudioViewModel : BaseViewModel, IConfigureStudioViewModel
    {
        public Estudio Estudio { get; set; }
    }

    public interface IConfigureStudioViewModel
    {
        Estudio Estudio { get; set; }
        List<Notificacion> Notificaciones { get; set; }
        bool MostrarNotificacion { get; set; }

        string TituloMensaje { get; set; }
        string SubtituloMensaje { get; set; }
    }
}