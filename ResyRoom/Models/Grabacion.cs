using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Grabacion
    {
        public int IdGrabacion { get; set; }
        public decimal? PrecioPorCancion { get; set; }
        public bool? Masterizacion { get; set; }
        public int? IdSala { get; set; }
        public DateTime? FechaIngreso { get; set; }
    }
}
