using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Reserva
    {
        public int IdReserva { get; set; }
        public int? IdHorario { get; set; }
        public int? IdBanda { get; set; }
        public int? IdEstadoRserva { get; set; }
        public DateTime? Fecha { get; set; }
        public string Desde { get; set; }
        public string Hasta { get; set; }
    
        public EstadoReserva EstadoReserva { get; set; }
        public Banda Banda { get; set; }
        public Horario Horario { get; set; }
    }
}
