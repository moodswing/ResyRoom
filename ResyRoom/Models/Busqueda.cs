﻿using System.ComponentModel;

namespace ResyRoom.Models
{
    public class Busqueda : IBusquedaRapida
    {
        #region >>> Implementción de IBusquedaRapida

        public string NombreEstudio { get; set; }
        public Comuna Comuna { get; set; }
        public Region Region { get; set; }
        public string PrecioDesde { get; set; }
        public string PrecioHasta { get; set; }
        [DisplayName("Instrumentos")]
        public bool ConInstrumentos { get; set; }
        [DisplayName("Doble Pedal")]
        public bool ConDoblePedal { get; set; }

        #endregion

        [DisplayName("Comentarios")]
        public bool ConComentarios { get; set; }
        [DisplayName("Fotos")]
        public bool ConFotos { get; set; }

        [DisplayName("Reserva Online")]
        public bool ConReservaOnline { get; set; }
        [DisplayName("Aire Acondicionado o Ventilacion")]
        public bool ConAireAcondicionadoVentilacion { get; set; }
        [DisplayName("Set de Platos")]
        public bool ConSetDePlatos { get; set; }
        [DisplayName("Nro. de Microfonos")]
        public int NroDeMicrofonos { get; set; }

        #region >>> Grabacion

        [DisplayName("Disponible para Grabaciones")]
        public bool ConGrabacion { get; set; }
        public decimal PrecioPorGrabacionDeCancionDesde { get; set; }
        public decimal PrecioPorGrabacionDeCancionHasta { get; set; }
        [DisplayName("Masterización")]
        public bool Masterizacion { get; set; }
        [DisplayName("Por Pistas")]
        public bool PorPistas { get; set; }
        [DisplayName("Exclusivamente de Grabación")]
        public bool ExclusivamenteGrabacion { get; set; }

        #endregion

    }
}