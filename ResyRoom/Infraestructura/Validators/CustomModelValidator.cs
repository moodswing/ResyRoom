using System.Linq;
using System.Web.Mvc;
using ResyRoom.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ResyRoom.Infraestructura.Validators
{
    public class CustomModelValidator : ModelValidator
    {
        public CustomModelValidator(ModelMetadata metadata, ControllerContext context)
            : base(metadata, context)
        {
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            var validationChecker = Metadata.Model as ValidationChecker;
            if (validationChecker != null && !validationChecker.IsValidated)
            {
                validationChecker.IsValidated = true;
                var model = container as BaseValidatableObject;
                var validatableObject = model;
                if (validatableObject == null)
                    return Enumerable.Empty<ModelValidationResult>();
                var validationContext = new ValidationContext(validatableObject, null, null);

                return ConvertResults(validatableObject.Validate(validationContext));
            }
            return Enumerable.Empty<ModelValidationResult>();
        }

        public IEnumerable<CustomValidationResult.CustomModelValidationResult> CustomValidate()
        {
            var model = Metadata.Model as BaseViewModel;
            if (model != null && !model.ValidatorTrigger.IsValidated)
            {
                model.ValidatorTrigger.IsValidated = true;
                var validatableObject = model as IValidatableObject;
                var validationContext = new ValidationContext(validatableObject, null, null);
                return ConvertResults(validatableObject.Validate(validationContext));
            }
            return Enumerable.Empty<CustomValidationResult.CustomModelValidationResult>();
        }

        private static IEnumerable<CustomValidationResult.CustomModelValidationResult> ConvertResults(IEnumerable<ValidationResult> results)
        {
            foreach (ValidationResult result in results)
            {
                if (result != ValidationResult.Success && (result as FlowValidationResult) != null)
                {
                    if (result.MemberNames == null || !result.MemberNames.Any())
                        yield return new CustomValidationResult.CustomModelValidationResult { Message = result.ErrorMessage, ValidationResultType = ((FlowValidationResult)result).ValidationResultType };
                    else
                    {
                        foreach (string memberName in result.MemberNames)
                            yield return new CustomValidationResult.CustomModelValidationResult { Message = result.ErrorMessage, MemberName = memberName, ValidationResultType = ((FlowValidationResult)result).ValidationResultType };

                    }
                }
            }
        }
    }
}