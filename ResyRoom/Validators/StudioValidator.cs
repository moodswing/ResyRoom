using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Microsoft.Practices.Unity;
using ResyRoom.Infraestructura;
using ResyRoom.Infraestructura.Validators;
using ResyRoom.Servicios;
using ResyRoom.ViewModels.RegisterStudio;

namespace ResyRoom.Validators
{
    public class StudioValidator : IValidator<RegisterStudioViewModel>
    {
        [Dependency]
        public IServUsuarios ServUsuarios { get; set; }

        [Dependency]
        public IServEstudios ServEstudios { get; set; }

        public IEnumerable<ValidationResult> Validate(RegisterStudioViewModel viewModel, ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();

            if (viewModel.PasoActual != 0)
            {
                switch (viewModel.PasoActual)
                {
                    case (int) RegisterStudioPage.AccountInfo:
                        if (ServUsuarios.EmailYaUsado(viewModel.Usuario.Email))
                            results.Add(new FlowValidationResult("El correo que intenta ingresar ya existe.", new List<string> { "Usuario.Email" }) { ValidationResultType = ValidationResultTypes.Critical });

                        break;
                    case (int) RegisterStudioPage.GeneralInfo:
                        if (ServEstudios.NombreEstudioYaUsado(viewModel.Estudio.Nombre))
                            results.Add(new FlowValidationResult("El nombre que intenta ingresar ya existe.", new List<string> { "Estudio.Nombre" }) { ValidationResultType = ValidationResultTypes.Critical });
                        if (ServEstudios.UrlEstudioYaUsado(viewModel.Estudio.UrlName))
                            results.Add(new FlowValidationResult("El nombre para la url que intenta ingresar ya existe.", new List<string> { "Estudio.UrlName" }) { ValidationResultType = ValidationResultTypes.Critical });

                        break;
                }
            }

            return results;
        }
    }
}