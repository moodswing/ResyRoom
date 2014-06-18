using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Horario
    {
        public Horario()
        {
            Ofertas = new HashSet<Oferta>();
        }
    
        public int IdHorario { get; set; }
        public int? IdSala { get; set; }
        public double? DuracionBloque { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string HoraApertura { get; set; }
        public string HoraCierre { get; set; }
        public string DíasAbierto { get; set; }
    
        public Sala Sala { get; set; }
        public ICollection<Oferta> Ofertas { get; set; }
    }
}
