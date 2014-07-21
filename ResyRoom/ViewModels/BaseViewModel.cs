using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResyRoom.Infraestructura;
using ResyRoom.Models;
using Estudio = ResyRoom.Models.Estudio;

namespace ResyRoom.ViewModels
{
    public abstract class BaseViewModel
    {
        public List<Notificacion> Notificaciones { get; set; }
        public bool MostrarNotificacion { get; set; }
        public IdentificacionDeUsuario LoginUsuario { get; set; }

        public IEnumerable<Estudio> EstudiosMejorEvaluados { get; set; }
        public IEnumerable<Estudio> EstudiosMasPopulares { get; set; }

        public string TituloMensaje { get; set; }
        public string SubtituloMensaje { get; set; }

        public bool UserLogged { get { return HttpContext.Current.User.Identity.IsAuthenticated; } }
    }
}