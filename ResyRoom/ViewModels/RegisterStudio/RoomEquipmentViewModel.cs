using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ResyRoom.Infraestructura;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class RoomEquipmentViewModel : BaseViewModel, IRoomEquipmentViewModel
    {
        [DisplayName("Nombre del equipo")]
        [Required(ErrorMessage = "Favor ingresar el nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Descripcion")]
        public string Descripcion { get; set; }

        [Display(Name = "Tiene un precio adicional")]
        public bool TieneUnPrecioAdicional { get; set; }

        [Display(Name = "Precio Adicional", Description = "Puedes especificar el valor adicional que posee este equipo")]
        public int? PrecioAdicional { get; set; }

        public string Fotografia { get; set; }

        public int Indice { get; set; }

        public EnumCollection.ViewState ViewState { get; set; }
    }

    public interface IRoomEquipmentViewModel
    {
        string Nombre { get; set; }
        string Descripcion { get; set; }
        int Indice { get; set; }
        bool TieneUnPrecioAdicional { get; set; }
        int? PrecioAdicional { get; set; }
        string Fotografia { get; set; }
        EnumCollection.ViewState ViewState { get; set; }
    }
}