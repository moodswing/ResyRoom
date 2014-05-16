using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Banda
    {
        public Banda()
        {
            Comentarios = new HashSet<Comentario>();
            Reservas = new HashSet<Reserva>();
        }
    
        public int IdBanda { get; set; }
        public int? IdGenero { get; set; }
    	[Required(ErrorMessage = "Dato faltante")]
        public string Nombre { get; set; }
    	[Required(ErrorMessage = "Dato faltante")]
    	[DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }
        public Guid? UserId { get; set; }
    
        public Genero Genero { get; set; }
        public ICollection<Comentario> Comentarios { get; set; }
        public ICollection<Reserva> Reservas { get; set; }
    }
}
