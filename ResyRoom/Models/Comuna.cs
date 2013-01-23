using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Comuna
    {
        public Comuna()
        {
            tb_Estudio = new HashSet<Estudio>();
        }
    
        public int IdComuna { get; set; }
        public int? IdRegion { get; set; }
        public string Descripcion { get; set; }
    
        public Region Region { get; set; }
        public ICollection<Estudio> tb_Estudio { get; set; }
    }
}
