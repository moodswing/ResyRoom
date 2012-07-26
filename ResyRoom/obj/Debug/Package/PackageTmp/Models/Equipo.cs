using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Equipo
    {
        public int IdEquipo { get; set; }
        public int? IdSala { get; set; }
        public int? IdTIpoEquipo { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public int? PrecioAdicional { get; set; }
    
        public TipoEquipo TipoEquipo { get; set; }
        public Sala Sala { get; set; }
    }
}
