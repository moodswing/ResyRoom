using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.Unity;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class UnityExt
    {
        public static IEnumerable<T> ResolveAll<T>(this IUnityContainer c, Func<string, bool> match)
        {
            var matches = c.Registrations.Where(r => match(r.Name));
            return matches.Select(r => c.Resolve<T>(r.Name));
        }
    }
}