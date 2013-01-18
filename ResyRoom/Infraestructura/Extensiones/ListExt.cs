using System.Linq;
using System.Web.Mvc;
using System.Collections;
using System.Collections.Generic;

namespace ResyRoom.Infraestructura.Extensiones
{
    public static class ListExt
    {
        public static IEnumerable<SelectListItem> ToSelectListItem(this IEnumerable lista)
        {
            var resultado = new List<SelectListItem>();

            foreach (var item in lista)
            {
                var descripcion = item.GetType().GetProperty("Descripcion").GetValue(item, null).ToString();
                var valor = item.GetType().GetProperties().Where(p => p.Name.Substring(0, 2) == "Id").First().GetValue(item, null).ToString();

                resultado.Add(new SelectListItem { Text = descripcion, Value = valor });
            }

            return resultado;
        }
    }
}