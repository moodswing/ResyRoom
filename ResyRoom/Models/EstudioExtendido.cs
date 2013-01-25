using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ResyRoom.Models
{
    public partial class Estudio
    {
        public string PrecioPorHoraDesde
        {
            get
            {
                return Convert.ToDecimal(Salas.Select(s => s.Precio).Min()).ToString("C0", CultureInfo.CreateSpecificCulture("es-CL"));
            }
        }
    }
}