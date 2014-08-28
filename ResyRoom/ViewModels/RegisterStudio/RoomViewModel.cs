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

        [Display(Name = "Tama�o2", Description = "Puede ingresar un aproximado, aunque entre mas exacto mejor")]
        public string Tama�o { get; set; }

        [Display(Name = "Posee esta sala un set de platos")]
        public bool SetDePlatos { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Favor ingresar el valor de la hora en la sala")]
        public string PrecioPorHora { get; set; }

        public EnumCollection.ViewModelState State { get; set; }

        public int Indice { get; set; }
    }

    public interface IRoomViewModel
    {
        string Nombre { get; set; }
        string Tama�o { get; set; }
        bool SetDePlatos { get; set; }
        string PrecioPorHora { get; set; }
        int Indice { get; set; }
    }
}