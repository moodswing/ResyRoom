using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Script.Serialization;

namespace ResyRoom.Infraestructura.Extensiones
{
    public class ExtendedJavaScriptConverter<T> : JavaScriptConverter where T : new()
    {
        private const string DateFormat = "dd/MM/yyyy";

        public override IEnumerable<Type> SupportedTypes
        {
            get
            {
                return new[] { typeof(T) };
            }
        }

        public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
        {
            var p = (T)obj;
            IDictionary<string, object> serialized = new Dictionary<string, object>();

            foreach (var pi in typeof(T).GetProperties())
            {
                if (pi.PropertyType == typeof(DateTime))
                    serialized[pi.Name] = ((DateTime)pi.GetValue(p, null)).ToString(DateFormat);
                else
                    serialized[pi.Name] = pi.GetValue(p, null);
            }

            return serialized;
        }

        public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
        {
            T p = new T();

            var props = typeof(T).GetProperties();

            foreach (string key in dictionary.Keys)
            {
                var prop = props.FirstOrDefault(t => t.Name == key);
                if (prop != null)
                {
                    if (prop.PropertyType == typeof(DateTime))
                    {
                        prop.SetValue(p, DateTime.ParseExact(dictionary[key] as string, DateFormat, DateTimeFormatInfo.InvariantInfo), null);

                    }
                    else
                    {
                        prop.SetValue(p, dictionary[key], null);
                    }
                }
            }

            return p;
        }

        public static JavaScriptSerializer GetSerializer()
        {
            var serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new[] { new ExtendedJavaScriptConverter<T>() });

            return serializer;
        }
    }

}