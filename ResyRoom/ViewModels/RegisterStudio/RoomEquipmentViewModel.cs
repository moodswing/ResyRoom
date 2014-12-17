using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ResyRoom.Infraestructura;
using ResyRoom.Infraestructura.CustomValidations;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class RoomEquipmentViewModel : BaseViewModel, IRoomEquipmentViewModel
    {
        [DisplayName("Nombre del equipo (*)")]
        [Required(ErrorMessage = "Favor ingresar el nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Tiene un precio adicional")]
        public bool TieneUnPrecioAdicional { get; set; }

        [Display(Name = "Precio Adicional", Description = "Puedes especificar el valor adicional que posee este equipo")]
        [RequiredIf("TieneUnPrecioAdicional", true, ErrorMessage = "Debes ingresar el precio adicional")]
        [DataType(DataType.Text)]
        public int? PrecioAdicional { get; set; }

        public string Fotografia { get; set; }

        [DisplayName("Equipo (*)")]
        [Required(ErrorMessage = "Favor ingresar el tipo de equipo")]
        [Range(1, int.MaxValue, ErrorMessage = "Favor ingresar el tipo de equipo")]
        public int IdTipoEquipo { get; set; }

        public ViewState ViewState { get; set; }
    }

    public interface IRoomEquipmentViewModel
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        int IndiceSeleccionado { get; set; }
        bool TieneUnPrecioAdicional { get; set; }
        int? PrecioAdicional { get; set; }
        string Fotografia { get; set; }
        int IdTipoEquipo { get; set; }
        ViewState ViewState { get; set; }
    }
}