using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Comentario
    {
        public int IdComentario { get; set; }
        public int? IdSala { get; set; }
        public int? IdBanda { get; set; }
        public short? Puntuacion { get; set; }
        public string Descripcion { get; set; }
        public DateTime? Fecha { get; set; }
    
        public Banda Banda { get; set; }
        public Sala Sala { get; set; }
    }
}
