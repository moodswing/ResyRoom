using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using ResyRoom.Models;
using Estudio = ResyRoom.Models.Estudio;

namespace ResyRoom.ViewModels
{
    public class ViewStudioViewModel : BaseViewModel, IViewStudioViewModel
    {
        public Estudio Estudio { get; set; }
        public Reserva Reserva { get; set; }
        public bool? ReservaRealizadaConExito { get; set; }
    }

    public interface IViewStudioViewModel
    {
        Estudio Estudio { get; set; }
        Reserva Reserva { get; set; }
        bool? ReservaRealizadaConExito { get; set; }
        IEnumerable<Estudio> EstudiosMejorEvaluados { get; set; }
        IEnumerable<Estudio> EstudiosMasPopulares { get; set; }
        List<Notificacion> Notificaciones { get; set; }
        bool MostrarNotificacion { get; set; }
    }
}