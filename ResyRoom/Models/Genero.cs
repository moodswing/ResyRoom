using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Genero
    {
        public Genero()
        {
            Bandas = new HashSet<Banda>();
        }
    
        public int IdGenero { get; set; }
        public string Descripcion { get; set; }
    
        public ICollection<Banda> Bandas { get; set; }
    }
}
