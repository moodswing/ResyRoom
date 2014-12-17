using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Infraestructura.Validators
{
    public interface IValidatorLocator
    {
        IEnumerable<Type> GetValidators();
    }

    public interface IValidator<T> where T : BaseValidatableObject
    {
        IEnumerable<ValidationResult> Validate(T viewModel, ValidationContext validationContext);
    }
}