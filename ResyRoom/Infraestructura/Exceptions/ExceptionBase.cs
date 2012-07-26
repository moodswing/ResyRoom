using System;
using System.Globalization;

namespace ResyRoom.Infraestructura.Exceptions
{
    [Serializable]
    public class ExceptionBase : Exception
    {
        public ExceptionBase(string mensaje) : base(mensaje) { }
        public ExceptionBase(string formato, params object[] valores) : base(string.Format(CultureInfo.CurrentCulture, formato, valores)) { }
        public ExceptionBase(Exception inner, string mensaje) : base(mensaje, inner) { }
        public ExceptionBase(Exception inner, string formato, params object[] valores) : base(string.Format(CultureInfo.CurrentCulture, formato, valores), inner) { }
    }
}