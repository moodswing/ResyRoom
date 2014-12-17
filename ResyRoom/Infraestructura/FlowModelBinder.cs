using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ResyRoom.Infraestructura.Validators;

namespace ResyRoom.Infraestructura
{
    public class FlowModelBinder : DefaultModelBinder
    {
        protected override void OnModelUpdated(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var customModelValidator = new CustomModelValidator(bindingContext.ModelMetadata, controllerContext);
            foreach (ModelValidationResult validationResult in customModelValidator.CustomValidate())
            {
                if ((validationResult as CustomValidationResult.CustomModelValidationResult) != null)
                {
                    var customMessage = new CustomModelError(validationResult.Message) { ValidationResultType = ((CustomValidationResult.CustomModelValidationResult)validationResult).ValidationResultType };
                    var subPropertyName = validationResult.MemberName;
                    if (bindingContext.ModelState.ContainsKey(subPropertyName))
                        bindingContext.ModelState[subPropertyName].Errors.Add(customMessage);
                    else
                    {
                        var modelState = new ModelState();
                        modelState.Errors.Add(customMessage);
                        bindingContext.ModelState.Add(subPropertyName, modelState);
                    }
                }
            }
        }
    }
}