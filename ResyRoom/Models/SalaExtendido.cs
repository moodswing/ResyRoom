using System.Linq;

namespace ResyRoom.Models
{
    public partial class Sala
    {
        public Grabacion DatosGrabacion
        {
            get { return Grabaciones.Any() ? Grabaciones.Last() : null; }
        }
    }
}