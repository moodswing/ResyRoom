﻿using System;
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
        #region >>> Dependencias

        [Dependency]
        public IServEstudios ServEstudios { get; set; }
        [Dependency]
        public IServRegiones ServRegiones { get; set; }
        [Dependency]
        public ISearchStudioViewModel SearchViewModel { get; set; }
        [Dependency]
        public IViewStudioViewModel ViewStudioViewModel { get; set; }

        #endregion

        #region >>> Configure Studio's view
        public ActionResult Configure(Estudio estudio)
        {
            return View(estudio);
        }
        #endregion

        #region >>> Search studios

        public ActionResult Search()
        {
            SearchViewModel.EstudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            SearchViewModel.EstudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);
            SearchViewModel.ListadoRegiones = ServRegiones.RegionesChilenas();
            SearchViewModel.ListadoComunas = new List<Comuna> {Constantes.ComunaSinSeleccion};
            SearchViewModel.Resultados = ServEstudios.Todas();
            
            return View(SearchViewModel);
        }

        [HttpPost]
        public ActionResult Search(Busqueda busqueda)
        {
            var resultados = ServEstudios.Busqueda(busqueda);

            return PartialView("_ResultadoEstudios", resultados);
        }

        #endregion

        #region >>> View studio

        public ActionResult ViewStudio(int id)
        {
            ViewStudioViewModel.EstudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            ViewStudioViewModel.EstudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);
            ViewStudioViewModel.Estudio = ServEstudios.CargarEstudio(id);

            return View(ViewStudioViewModel);
        }

        #endregion
    }
}
