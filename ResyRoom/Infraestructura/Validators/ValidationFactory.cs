using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using Microsoft.Practices.Unity;
using ResyRoom.Servicios;
using ResyRoom.ViewModels;

namespace ResyRoom.Infraestructura.Validators
{
    public class ValidationFactory
    {
        private static bool _loadedValidators;
        private static IValidatorLocator _defaultLocator = new DefaultValidatorLocator();
        private static readonly ConcurrentDictionary<Type, Func<BaseValidatableObject, ValidationContext, IEnumerable<ValidationResult>>> Validators = new ConcurrentDictionary<Type, Func<BaseValidatableObject, ValidationContext, IEnumerable<ValidationResult>>>();

        public static void RegisterLocator(IValidatorLocator locator)
        {
            if (locator == null)
                throw new ArgumentNullException("locator", "Cannot set locator with a null reference");

            _defaultLocator = locator;

            var validatorTypes = locator.GetValidators();
            if (validatorTypes != null)
            {
                foreach (var validatorType in validatorTypes)
                {
                    var validator = Activator.CreateInstance(validatorType);

                    if (validatorType.GetInterfaces().Any())
                    {
                        var unityValidator = DependencyResolver.Current.GetService(validatorType.GetInterfaces()[0]);
                        if (unityValidator != null)
                            validator = unityValidator;
                    }

                    var mi = validator.GetType().GetMethod("Validate");

                    Func<BaseValidatableObject, ValidationContext, IEnumerable<ValidationResult>> func = (x, y) => mi.Invoke(validator, new object[] { x, y }) as IEnumerable<ValidationResult>;

                    var genericType = validatorType.GetInterfaces().Where(x => x.IsGenericType).Select(x => x.GetGenericArguments().FirstOrDefault()).FirstOrDefault();
                    if (genericType != null && !Validators.ContainsKey(genericType))
                        Validators.TryAdd(genericType, func);
                }
            }

            _loadedValidators = true;
        }

        public static Func<BaseValidatableObject, ValidationContext, IEnumerable<ValidationResult>> GetValidator(Type viewModelType)
        {
            if (!_loadedValidators && !Validators.Any())
                RegisterLocator(_defaultLocator);

            if (Validators.ContainsKey(viewModelType))
                return Validators[viewModelType];

            return null;
        }
    }
}