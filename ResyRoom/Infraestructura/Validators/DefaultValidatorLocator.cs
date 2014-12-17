using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace ResyRoom.Infraestructura.Validators
{
    public class DefaultValidatorLocator : IValidatorLocator
    {
        public IEnumerable<Type> GetValidators()
        {
            var types = Assembly.GetEntryAssembly() != null ? Assembly.GetEntryAssembly().GetTypes() : GetWebEntryAssemblyTypes();
            if (types != null)
            {
                var validatorTypes = (from t in types
                                      from i in t.GetInterfaces()
                                      where i.IsGenericType &&
                                            i.GetGenericTypeDefinition() == typeof(IValidator<>)
                                      select t);

                var list = validatorTypes.ToList();
                return !list.Any() ? null : list.ToArray();
            }

            return new List<Type>().ToArray();
        }

        private static Type[] GetWebEntryAssemblyTypes()
        {
            if (HttpContext.Current != null)
            {
                var webApplicationType = HttpContext.Current.ApplicationInstance.GetType();

                while (webApplicationType != null && webApplicationType.Namespace == "ASP")
                    webApplicationType = webApplicationType.BaseType;

                return webApplicationType != null ? webApplicationType.Assembly.GetTypes() : null;
            }

            return null;
        }
    }
}