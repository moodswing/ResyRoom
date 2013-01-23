using System.ComponentModel;
using ResyRoom.Models.Enums;

namespace ResyRoom.Models
{
    public class BusquedaRapida
    {
        public string NombreEstudio { get; set; }
        public Comuna Comuna { get; set; }
        public Region Region { get; set; }
        public string PrecioDesde { get; set; }
        public string PrecioHasta { get; set; }
        [DisplayName("Con Instrumentos")]
        public bool ConInstrumentos { get; set; }
        [DisplayName("Con Doble Pedal")]
        public bool ConDoblePedal { get; set; }
    }

    public interface IBusquedaRapida
    {
        string NombreEstudio { get; set; }
        Comuna Comuna { get; set; }
        Region Region { get; set; }
        string PrecioDesde { get; set; }
        string PrecioHasta { get; set; }
        bool ConInstrumentos { get; set; }
        bool ConDoblePedal { get; set; }
    }
}
