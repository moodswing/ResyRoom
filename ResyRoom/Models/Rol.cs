using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Models
{
    public class Rol
    {
        public Rol()
        {
            Usuarios = new HashSet<Usuario>();
        }
    
        public int IdRol { get; set; }
        public string Nombre { get; set; }
        public string Description { get; set; }
    
        public ICollection<Usuario> Usuarios { get; set; }
    }
}
