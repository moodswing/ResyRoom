using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Region
    {
        public Region()
        {
            Comunas = new HashSet<Comuna>();
        }
    
        public int IdRegion { get; set; }
        public int? IdPais { get; set; }
        public string Descripcion { get; set; }
    
        public ICollection<Comuna> Comunas { get; set; }
        public Pais Pais { get; set; }
    }
}
