using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ResyRoom.Infraestructura;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class RoomViewModel : BaseViewModel, IRoomViewModel
    {
        [DisplayName("Nombre de la sala")]
        [Required(ErrorMessage = "Favor ingresar su nombre")]
        public string Nombre { get; set; }

        [Display(Name = "Tamaño", Description = "Puede ingresar un aproximado, aunque entre mas exacto mejor")]
        public string Tamaño { get; set; }

        [Display(Name = "Posee esta sala un set de platos")]
        public bool SetDePlatos { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Favor ingresar el valor de la hora en la sala")]
        public string PrecioPorHora { get; set; }

        public bool SinEquipos { get; set; }

        public List<RoomEquipmentViewModel> Equipos { get; set; }

        public RoomViewModel()
        {
            Equipos = new List<RoomEquipmentViewModel>();
        }
    }

    public interface IRoomViewModel
    {
        string Nombre { get; set; }
        string Tamaño { get; set; }
        bool SetDePlatos { get; set; }
        string PrecioPorHora { get; set; }
        
        int Indice { get; set; }
        int IndiceSeleccionado { get; set; }

        bool SinEquipos { get; set; }

        List<RoomEquipmentViewModel> Equipos { get; set; }
    }
}