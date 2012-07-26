using ResyRoom.Models;
using System.Collections.Generic;

namespace ResyRoom.ViewModels
{
    public class IndexUserViewModel
    {
        public IdentificacionDeUsuario Usuario { get; set; }

        public IEnumerable<Estudio> EstudiosMejorEvaluados { get; set; }
        public IEnumerable<Estudio> EstudiosMasPopulares { get; set; }

        public IndexUserViewModel()
        {
            Usuario = new IdentificacionDeUsuario();
            EstudiosMejorEvaluados = new List<Estudio>();
            EstudiosMasPopulares = new List<Estudio>();
        }

        public IndexUserViewModel(IdentificacionDeUsuario usuario, IEnumerable<Estudio> estudiosMejorEvaludos, IEnumerable<Estudio> estudiosMasPopulares)
        {
            Usuario = usuario;
            EstudiosMejorEvaluados = estudiosMejorEvaludos;
            EstudiosMasPopulares = estudiosMasPopulares;
        }
    }
}