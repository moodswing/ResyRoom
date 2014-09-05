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

        public RoomScheduleViewModel Horario { get; set; }
        public List<RoomEquipmentViewModel> Equipos { get; set; }

        public RoomViewModel()
        {
            Horario = new RoomScheduleViewModel
                {
                    HoraApertura = "11 : 00",
                    HoraCierre = "22 : 00",
                    DuracionBloque = "01 : 00",
                    DiasAbierto = "123456"
                };

            Equipos = new List<RoomEquipmentViewModel>();
        }
    }

    public interface IRoomViewModel
    {
        string Nombre { get; set; }
        string Tamaño { get; set; }
        bool SetDePlatos { get; set; }
        string PrecioPorHora { get; set; }
        
        int IndiceSeleccionado { get; set; }

        bool SinEquipos { get; set; }

        RoomScheduleViewModel Horario { get; set; }
        List<RoomEquipmentViewModel> Equipos { get; set; }
    }
}