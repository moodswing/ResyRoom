using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResyRoom.Models
{
    public partial class Estudio
    {
        public decimal PrecioPorHoraDesde
        {
            get
            {
                decimal valor = 0;
                valor = Convert.ToDecimal(Salas.Select(s => s.Precio).Min());

                return valor;
            }
        }
    }
}