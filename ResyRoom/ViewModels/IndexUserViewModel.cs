using ResyRoom.Models;
using System.Collections.Generic;

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
            Usuario = new IdentificacionDeUsuario();
            EstudiosMejorEvaluados = new List<Estudio>();
            EstudiosMasPopulares = new List<Estudio>();
            BusquedaRapida = new BusquedaRapida();
        }

        public IndexUserViewModel(IdentificacionDeUsuario usuario, 
            IEnumerable<Estudio> estudiosMejorEvaludos, IEnumerable<Estudio> estudiosMasPopulares, 
            IEnumerable<Region> listadoRegiones, IEnumerable<Comuna> listadoComunas)
        {
            Usuario = usuario;
            EstudiosMejorEvaluados = estudiosMejorEvaludos;
            EstudiosMasPopulares = estudiosMasPopulares;

            ListadoRegiones = listadoRegiones;
            ListadoComunas = listadoComunas;

            BusquedaRapida = new BusquedaRapida();
        }
    }
}