using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class IntExt
    {
        public static string ToCurrency(this int number)
        {
            return "$" + number.ToString("N0", CultureInfo.GetCultureInfo("de"));
        }

        public static string ToCurrency(this int? number)
        {
            return "$" + (number ?? 0).ToString("N0", CultureInfo.GetCultureInfo("de"));
        }

        public static string ToCurrency(this decimal? number)
        {
            return "$" + (number ?? 0).ToString("N0", CultureInfo.GetCultureInfo("de"));
        }
    }
}