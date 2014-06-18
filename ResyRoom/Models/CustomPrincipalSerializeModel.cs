using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResyRoom.Models
{
    public class CustomPrincipalSerializeModel
    {
        public int IdUsuario { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string FacebookId { get; set; }
        public bool IsFacebookLogin { get; set; }
        public string[] Roles { get; set; }
    }
}