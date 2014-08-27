using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class StudioViewModel : BaseViewModel, IStudioViewModel
    {
        [DisplayName("Nombre del Estudio")]
        [Required(ErrorMessage = "Favor ingresar su nombre")]
        public string Nombre { get; set; }
        
        [DisplayName("Direcci�n")]
        [Required(ErrorMessage = "Favor ingresar la direcci�n del estudio")]
        public string Direccion { get; set; }
        
        [DisplayName("Email de Contacto")]
        [Required(ErrorMessage = "Favor ingresar el email de contacto del estudio")]
        public string Email { get; set; }
        
        [DisplayName("Tel�fono")]
        public string Telefono { get; set; }
        
        [DisplayName("Tel�fono Movil")]
        public string Celular { get; set; }

        [DisplayName("Nombre de la Url")]
        [Display(Name = "Nombre de la Url", Description = "Para un acceso mas f�cil elige un nombre, ej. resyroom.com/tuEstudio")]
        [Required(ErrorMessage = "Favor ingresar la url de acceso del estudio")]
        public string UrlName { get; set; }
        
        public string SitioWeb { get; set; }
    }

    public interface IStudioViewModel
    {
        
    }
}