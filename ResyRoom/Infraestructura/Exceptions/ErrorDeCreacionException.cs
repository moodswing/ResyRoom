using System;

namespace ResyRoom.Infraestructura.Exceptions
{
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1032")]
    [Serializable]
    public class ErrorDeCreacionException : ExceptionBase
    {
        public ErrorDeCreacionException(string mensaje) : base(mensaje) { }
        public ErrorDeCreacionException(string formato, params object[] valores) : base(formato, valores) { }
        public ErrorDeCreacionException(Exception inner, string mensaje) : base(inner, mensaje) { }
        public ErrorDeCreacionException(Exception inner, string formato, params object[] valores) : base(inner, formato, valores) { }
    }
}
