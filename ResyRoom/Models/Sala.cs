using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public partial class Sala
    {
        public Sala()
        {
            Comentarios = new HashSet<Comentario>();
            Reservas = new HashSet<Reserva>();
            Grabaciones = new HashSet<Grabacion>();
            Equipos = new HashSet<Equipo>();
            Horarios = new HashSet<Horario>();
        }
    
        public int IdSala { get; set; }
        public int? IdEstudio { get; set; }
        public string Nombre { get; set; }
        public string Tamaño { get; set; }
        public bool? PrecioUnico { get; set; }
        public int? Precio { get; set; }
        public bool? DoblePedal { get; set; }
        public bool? SetDePlatos { get; set; }
    
        public ICollection<Comentario> Comentarios { get; set; }
        public Estudio Estudio { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
        public ICollection<Grabacion> Grabaciones { get; set; }
        public ICollection<Equipo> Equipos { get; set; }
        public ICollection<Horario> Horarios { get; set; }
    }
}
