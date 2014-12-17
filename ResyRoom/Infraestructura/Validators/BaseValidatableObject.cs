using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Infraestructura.Validators
{
    public abstract class BaseValidatableObject : IValidatableObject
    {
        public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var validator = ValidationFactory.GetValidator(GetType());
            return validator != null ? validator(this, validationContext) : new List<ValidationResult>();
        }
    }
}