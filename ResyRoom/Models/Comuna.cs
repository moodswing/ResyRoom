using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Comuna
    {
        public int IdComuna { get; set; }
        public int? IdRegion { get; set; }
        public string Descripcion { get; set; }
    
        public Region Region { get; set; }
    }
}
