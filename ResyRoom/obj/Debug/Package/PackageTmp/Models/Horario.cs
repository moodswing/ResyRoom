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
            Reserva = new HashSet<Reserva>();
        }
    
        public int IdHorario { get; set; }
        public int? IdSala { get; set; }
        public double? DuracionBloque { get; set; }
        public DateTime? FechaIngreso { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
    
        public Sala Sala { get; set; }
        public ICollection<Oferta> Ofertas { get; set; }
        public ICollection<Reserva> Reserva { get; set; }
    }
}
