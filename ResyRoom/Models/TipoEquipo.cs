using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class TipoEquipo
    {
        public TipoEquipo()
        {
            Equipo = new HashSet<Equipo>();
        }
    
        public int IdTipoEquipo { get; set; }
        public string Descripcion { get; set; }
    
        public ICollection<Equipo> Equipo { get; set; }
    }
}
