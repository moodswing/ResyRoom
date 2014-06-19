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
    }

    public interface IViewStudioViewModel
    {
        Estudio Estudio { get; set; }
        IEnumerable<Estudio> EstudiosMejorEvaluados { get; set; }
        IEnumerable<Estudio> EstudiosMasPopulares { get; set; }
    }
}