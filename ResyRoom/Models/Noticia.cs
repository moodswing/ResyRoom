using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Noticia
    {
        public int IdNoticia { get; set; }
        public int? IdEstudio { get; set; }
        public int? IdBanda { get; set; }
        public string Titulo { get; set; }
        public string Contenido { get; set; }
        public DateTime? FechaIngreso { get; set; }
    
        public Estudio Estudio { get; set; }
    }
}
