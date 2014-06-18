using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Grabacion
    {
        public Grabacion()
        {
            Sala = new HashSet<Sala>();
        }
    
        public int IdGrabacion { get; set; }
        public decimal? PrecioPorCancion { get; set; }
        public bool? Masterizacion { get; set; }
        public bool? SoloGrabacion { get; set; }
    
        public ICollection<Sala> Sala { get; set; }
    }
}
