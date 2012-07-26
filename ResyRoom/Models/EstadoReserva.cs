using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class EstadoReserva
    {
        public EstadoReserva()
        {
            Reservas = new HashSet<Reserva>();
        }
    
        public int IdEstadoReserva { get; set; }
        public string Descripcion { get; set; }
    
        public ICollection<Reserva> Reservas { get; set; }
    }
}
