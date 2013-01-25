using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ResyRoom.Models;
using ResyRoom.Servicios;
using ResyRoom.ViewModels;

namespace ResyRoom.Controllers
{
    public class StudioController : Controller
    {
        #region >>> Servicios
        [Dependency]
        public IServEstudios ServEstudios { get; set; }
        [Dependency]
        public IServEmail ServEmail { get; set; }
        [Dependency]
        public IServRegiones ServRegiones { get; set; }
        [Dependency]
        public IServGeneros ServGeneros { get; set; }
        [Dependency]
        public IServBandas ServBandas { get; set; }
        [Dependency]
        public IServUsuarios ServUsuarios { get; set; }
        [Dependency]
        public IServComunas ServComunas { get; set; }
        #endregion
        #region >>> Configure Studio's view
        public ActionResult Configure(Estudio estudio)
        {
            return View(estudio);
        }
        #endregion

        public ActionResult Search()
        {
            var usuario = new IdentificacionDeUsuario();
            var estudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            var estudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);

            var listadoRegiones = ServRegiones.RegionesChilenas();
            var listadoComunas = new List<Comuna> { Constantes.ComunaSinSeleccion };

            var resultados = ServEstudios.Todas();

            return View(new SearchStudioViewModel(usuario, estudiosMejorEvaluados, estudiosMasPopulares, listadoRegiones, listadoComunas, resultados));
        }

        [HttpPost]
        public ActionResult Search(Busqueda busqueda)
        {
            var resultados = ServEstudios.Busqueda(busqueda);

            return PartialView("_ResultadoEstudios", resultados);
        }
    }
}
