using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Estudio
    {
        public Estudio()
        {
            Noticias = new HashSet<Noticia>();
            Salas = new HashSet<Sala>();
        }
    
        public int IdEstudio { get; set; }
    	[DisplayName("Comuna")]
        public int? IdComuna { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
    	[DisplayName("Email de Contacto")]
        public string Email { get; set; }
    	[DisplayName("Teléfono")]
        public string Telefono { get; set; }
    	[DisplayName("Teléfono Movil")]
        public string Celular { get; set; }
        public Guid? UserId { get; set; }
    
    	public int NroDeReservas { get; set; }
    
        public User Usuario { get; set; }
        public ICollection<Noticia> Noticias { get; set; }
        public ICollection<Sala> Salas { get; set; }
    }
}
