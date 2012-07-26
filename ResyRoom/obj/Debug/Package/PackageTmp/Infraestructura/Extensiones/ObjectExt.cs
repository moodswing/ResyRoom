using System.Collections.Generic;
using System.Linq;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class ObjectExt
    {
        public static bool NotIn<T>(this T extended, IEnumerable<T> filter)
        {
            return !extended.In(filter);
        }

        public static bool In<T>(this T extended, IEnumerable<T> filter)
        {
            return filter.Contains(extended);
        }
    }
}