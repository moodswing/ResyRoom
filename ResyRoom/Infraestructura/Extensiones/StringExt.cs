using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class StringExt
    {
        public static bool IsEmptyOrWs(this string texto)
        {
            return String.IsNullOrWhiteSpace(texto);
        }
    }
}