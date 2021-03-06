﻿using ResyRoom.Models;
using System.Collections.Generic;
using Estudio = ResyRoom.Models.Estudio;

namespace ResyRoom.ViewModels
{
    public class IndexUserViewModel : BaseViewModel
    {
        public IEnumerable<Estudio> EstudiosMejorEvaluados { get; set; }
        public IEnumerable<Estudio> EstudiosMasPopulares { get; set; }
        public IEnumerable<Comuna> ListadoComunas { get; set; }
        public IEnumerable<Region> ListadoRegiones { get; set; }

        public BusquedaRapida BusquedaRapida { get; set; }

        public IndexUserViewModel()
        {
            EstudiosMejorEvaluados = new List<Estudio>();
            EstudiosMasPopulares = new List<Estudio>();
            BusquedaRapida = new BusquedaRapida();
        }

        public IndexUserViewModel(IEnumerable<Estudio> estudiosMejorEvaludos, IEnumerable<Estudio> estudiosMasPopulares, 
            IEnumerable<Region> listadoRegiones, IEnumerable<Comuna> listadoComunas)
        {
            EstudiosMejorEvaluados = estudiosMejorEvaludos;
            EstudiosMasPopulares = estudiosMasPopulares;

            ListadoRegiones = listadoRegiones;
            ListadoComunas = listadoComunas;

            BusquedaRapida = new BusquedaRapida();
        }
    }
}