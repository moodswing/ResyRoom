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
        [DisplayName("Nombre del Estudio")]
        [Required(ErrorMessage = "Favor ingresar su nombre")]
        public string Nombre { get; set; }
        
        [DisplayName("Dirección")]
        [Required(ErrorMessage = "Favor ingresar la dirección del estudio")]
        public string Direccion { get; set; }
        
        [DisplayName("Email de Contacto")]
        [DataType(DataType.EmailAddress)]
        [Required(ErrorMessage = "Favor ingresar el email de contacto del estudio")]
        public string Email { get; set; }
        
        [DisplayName("Teléfono")]
        public string Telefono { get; set; }
        
        [DisplayName("Teléfono Movil")]
        public string Celular { get; set; }

        [Display(Name = "Nombre de la Url", Description = "Para un acceso mas fácil elige un nombre, ej. resyroom.com/tuEstudio")]
        [Required(ErrorMessage = "Favor ingresar la url de acceso del estudio")]
        public string UrlName { get; set; }
        
        public string SitioWeb { get; set; }

        public IList<RoomViewModel> Salas { get; set; }

        public StudioViewModel()
        {
            Salas = new List<RoomViewModel>
                {
                    new RoomViewModel { State = EnumCollection.ViewModelState.Edit }
                };
        }
    }

    public interface IStudioViewModel
    {
        
    }
}