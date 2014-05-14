using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ResyRoom.Models;

namespace ResyRoom.ViewModels
{
    public abstract class BaseViewModel
    {
        public IdentificacionDeUsuario Usuario { get; set; }
    }
}