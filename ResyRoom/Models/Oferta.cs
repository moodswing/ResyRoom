using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Oferta
    {
        public int IdOferta { get; set; }
        public int? IdHorario { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Desde { get; set; }
        public DateTime? Hasta { get; set; }
        public int? Porcentaje { get; set; }
        public int? NuevoPrecio { get; set; }
    
        public Horario Horario { get; set; }
    }
}
