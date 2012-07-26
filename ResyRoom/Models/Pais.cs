using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Pais
    {
        public Pais()
        {
            Regiones = new HashSet<Region>();
        }
    
        public int IdPais { get; set; }
        public string Descripcion { get; set; }
    
        public ICollection<Region> Regiones { get; set; }
    }
}
