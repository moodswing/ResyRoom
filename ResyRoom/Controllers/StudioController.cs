﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ResyRoom.Models;
using ResyRoom.Servicios;
using ResyRoom.ViewModels;
using Estudio = ResyRoom.Models.Estudio;

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
        public IServReservas ServReservas { get; set; }
        [Dependency]
        public ISearchStudioViewModel SearchViewModel { get; set; }
        [Dependency]
        public IViewStudioViewModel ViewStudioViewModel { get; set; }
        [Dependency]
        public IConfigureStudioViewModel ConfigureStudioViewModel { get; set; }

        #endregion

        #region >>> Configure Studio's view
        public ActionResult Configure()
        {
            return View(ConfigureStudioViewModel);
        }
        #endregion

        #region >>> Search studios

        public ActionResult Search()
        {
            SearchViewModel.EstudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            SearchViewModel.EstudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);
            SearchViewModel.ListadoRegiones = ServRegiones.RegionesChilenas();
            SearchViewModel.ListadoComunas = new List<Comuna> { Constantes.ComunaSinSeleccion };
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

        [HttpGet]
        public ActionResult ViewStudio(string id)
        {
            int idNumber;
            var isId = int.TryParse(id, out idNumber);

            ViewStudioViewModel.EstudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            ViewStudioViewModel.EstudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);

            if (isId)
                ViewStudioViewModel.Estudio = ServEstudios.CargarEstudioPorId(idNumber);
            else
                ViewStudioViewModel.Estudio = ServEstudios.CargarEstudioPorUrl(id);

            ViewStudioViewModel.Notificaciones = new List<Notificacion> { new Notificacion() };

            var reserva = (ViewStudioViewModel)TempData["ReservaModel"];
            if (reserva != null)
            {
                ViewStudioViewModel.ReservaRealizadaConExito = reserva.ReservaRealizadaConExito;

                if (ViewStudioViewModel.ReservaRealizadaConExito == true)
                {
                    ViewStudioViewModel.TituloMensaje = Constantes.ReservaRealizadaConExito;
                    ViewStudioViewModel.SubtituloMensaje = Constantes.SubtituloReservaRealizadaConExito;
                }
                else
                {
                    ViewStudioViewModel.TituloMensaje = Constantes.ReservaFallida;
                    ViewStudioViewModel.SubtituloMensaje = Constantes.SubtituloReservaFallida;
                }
            }

            return View(ViewStudioViewModel);
        }

        [HttpPost]
        public ActionResult ViewStudio(ViewStudioViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.Reserva == null || model.Reserva.Fecha == null)
                    ViewStudioViewModel.ReservaRealizadaConExito = false;
                else
                    ViewStudioViewModel.ReservaRealizadaConExito = ServReservas.Guardar(model.Reserva);

                TempData["ReservaModel"] = ViewStudioViewModel;

                return RedirectToAction("ViewStudio", "Studio", new { id = model.Estudio.IdEstudio });
            }

            ViewStudioViewModel.EstudiosMejorEvaluados = ServEstudios.EstudiosMejorEvaluados(5);
            ViewStudioViewModel.EstudiosMasPopulares = ServEstudios.EstudiosMasPopulares(5);

            if (model.Reserva.IdSala != null)
                ViewStudioViewModel.Estudio = ServEstudios.CargarEstudioPorId((int)model.Reserva.IdSala);

            return View(ViewStudioViewModel);
        }

        public ActionResult LoadViewStudioTab(int idEstudio, string view)
        {
            ViewStudioViewModel.Estudio = ServEstudios.CargarEstudioPorId(idEstudio);

            return PartialView(view, ViewStudioViewModel);
        }
        #endregion
    }
}
