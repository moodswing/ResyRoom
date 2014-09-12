using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using ResyRoom.Infraestructura;
using ResyRoom.Models;

namespace ResyRoom.ViewModels.RegisterStudio
{
    public class RoomScheduleViewModel : BaseViewModel, IRoomScheduleViewModel
    {
        [Display(Name = "Duraci�n de un bloque (*)", Description = "Este es el m�nimo que se podr� arrendar esta sala")]
        [Required(ErrorMessage = "Favor ingresar la duraci�n del bloque a reservar")]
        public string DuracionBloque { get; set; }

        [Display(Name = "Hora de apertura (*)", Description = "Hora desde la cual se puede reservar en esta sala")]
        [Required(ErrorMessage = "Favor ingresar la hora de apertura de esta sala")]
        public string HoraApertura { get; set; }

        [Display(Name = "Hora de cierre (*)", Description = "Hora hasta la cual se puede reservar en esta sala")]
        [Required(ErrorMessage = "Favor ingresar la hora de cierra de esta sala")]
        public string HoraCierre { get; set; }

        [Display(Name = "D�as abiertos (*)")]
        [Required(ErrorMessage = "Favor ingresar los d�as en que estar� disponible esta sala")]
        public string DiasAbierto { get; set; }

        [Display(Name = "Precio por bloque (*)")]
        [Required(ErrorMessage = "Favor ingresar el precio por un bloque de tiempo")]
        public string PrecioPorBloque { get; set; }
    }

    public interface IRoomScheduleViewModel
    {
        string DuracionBloque { get; set; }
        string HoraApertura { get; set; }
        string HoraCierre { get; set; }
        string DiasAbierto { get; set; }
    }
}