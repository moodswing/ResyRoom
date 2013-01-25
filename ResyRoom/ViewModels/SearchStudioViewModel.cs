using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using ResyRoom.Models;

namespace ResyRoom.ViewModels
{
    public class SearchStudioViewModel
    {
        public IdentificacionDeUsuario Usuario { get; set; }

        public IEnumerable<Estudio> EstudiosMejorEvaluados { get; set; }
        public IEnumerable<Estudio> EstudiosMasPopulares { get; set; }
        public IEnumerable<Comuna> ListadoComunas { get; set; }
        public IEnumerable<Region> ListadoRegiones { get; set; }

        public Busqueda Busqueda { get; set; }
        public IEnumerable<Estudio> Resultados { get; set; }

        public SearchStudioViewModel()
        {
            Usuario = new IdentificacionDeUsuario();
            EstudiosMejorEvaluados = new List<Estudio>();
            EstudiosMasPopulares = new List<Estudio>();
            Busqueda = new Busqueda();
            Resultados = new List<Estudio>();
        }

        public SearchStudioViewModel(IdentificacionDeUsuario usuario, 
            IEnumerable<Estudio> estudiosMejorEvaludos, IEnumerable<Estudio> estudiosMasPopulares,
            IEnumerable<Region> listadoRegiones, IEnumerable<Comuna> listadoComunas, IEnumerable<Estudio> resultados)
        {
            Usuario = usuario;
            EstudiosMejorEvaluados = estudiosMejorEvaludos;
            EstudiosMasPopulares = estudiosMasPopulares;

            ListadoRegiones = listadoRegiones;
            ListadoComunas = listadoComunas;

            Resultados = resultados;

            

            Busqueda = new Busqueda();
        }
    }
}