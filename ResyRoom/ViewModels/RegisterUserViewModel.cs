using ResyRoom.Models;
using System.Collections.Generic;

namespace ResyRoom.ViewModels
{
    public class RegisterUserViewModel : BaseViewModel
    {
        public RegistroDeUsuario RegistroUsuario { get; private set; }
        public bool? RegistroExitoso { get; set; }
        public string MensajeError { get; set; }
    }
}