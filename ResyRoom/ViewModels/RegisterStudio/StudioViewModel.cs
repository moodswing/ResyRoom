using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ResyRoom.Infraestructura;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class StudioViewModel : BaseViewModel, IStudioViewModel
    {
        [DisplayName("Nombre del Estudio (*)")]
        [Required(ErrorMessage = "Favor ingresar su nombre")]
        public string Nombre { get; set; }

        [DisplayName("Direcci�n (*)")]
        [Required(ErrorMessage = "Favor ingresar la direcci�n del estudio")]
        public string Direccion { get; set; }

        [DisplayName("Regi�n (*)")]
        [Range(1, int.MaxValue, ErrorMessage = "Favor ingresar la regi�n en que se ubica del estudio")]
        [Required(ErrorMessage = "Favor ingresar la regi�n en que se ubica del estudio")]
        public int IdRegion { get; set; }

        [DisplayName("Comuna (*)")]
        [Range(1, int.MaxValue, ErrorMessage = "Favor ingresar la comuna en que se ubica del estudio")]
        [Required(ErrorMessage = "Favor ingresar la comuna en que se ubica del estudio")]
        public int IdComuna { get; set; }

        [DisplayName("Email de Contacto (*)")]
        [Required(ErrorMessage = "Favor ingresar el email de contacto del estudio")]
        [EmailAddress]
        public string Email { get; set; }
        
        [DisplayName("Tel�fono")]
        public string Telefono { get; set; } 
        
        [DisplayName("Tel�fono Movil")]
        public string Celular { get; set; }

        [Display(Name = "Nombre de la Url (*)", Description = "Para un acceso mas f�cil elige un nombre, ej. resyroom.com/tuEstudio")]
        [Required(ErrorMessage = "Favor ingresar la url de acceso del estudio")]
        public string UrlName { get; set; }

        [Display(Name = "Sitio Web")]
        public string SitioWeb { get; set; }

        public IList<RoomViewModel> Salas { get; set; }

        public StudioViewModel()
        {
            Salas = new List<RoomViewModel> { new RoomViewModel() };
        }
    }

    public interface IStudioViewModel
    {
        
    }
}