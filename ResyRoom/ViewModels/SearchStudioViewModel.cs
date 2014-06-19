using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using Microsoft.Practices.Unity;
using ResyRoom.Models;
using Estudio = ResyRoom.Models.Estudio;

namespace ResyRoom.ViewModels
{
    public class SearchStudioViewModel : BaseViewModel, ISearchStudioViewModel
    {
        [Dependency]
        public IEnumerable<Comuna> ListadoComunas { get; set; }
        public IEnumerable<Region> ListadoRegiones { get; set; }

        public Busqueda Busqueda { get; set; }
        public IEnumerable<Estudio> Resultados { get; set; }
    }

    public interface ISearchStudioViewModel
    {
        IEnumerable<Comuna> ListadoComunas { get; set; }
        IEnumerable<Region> ListadoRegiones { get; set; }

        IEnumerable<Estudio> EstudiosMejorEvaluados { get; set; }
        IEnumerable<Estudio> EstudiosMasPopulares { get; set; }

        Busqueda Busqueda { get; set; }
        IEnumerable<Estudio> Resultados { get; set; }
    }
}