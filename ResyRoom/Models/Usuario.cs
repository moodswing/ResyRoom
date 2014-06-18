using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Usuario
    {
        public Usuario()
        {
            Roles = new HashSet<Rol>();
        }
    
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool? EntraPorFacebook { get; set; }
        public string FacebookId { get; set; }
        public DateTime? SessionDate { get; set; }
    
        public ICollection<Rol> Roles { get; set; }
    }
}
