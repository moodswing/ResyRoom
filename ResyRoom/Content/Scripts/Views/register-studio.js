var RegisterStudioParameters = function (stepNumber, email, nombre, password, confirmarPassword, emailContacto, telefono, celular, nombreEstudio, sitioWeb, urlName) {
    return {
        StepNumber: stepNumber,
        Email: email,
        Nombre: nombre,
        Password: password,
        ConfirmarPassword: confirmarPassword,
        EmailContacto: emailContacto,
        Telefono: telefono,
        Celular: celular,
        NombreEstudio: nombreEstudio,
        SitioWeb: sitioWeb,
        UrlName: urlName
    };
};

var RegisterStudioConfiguration = function () {
    var init = function() {
        var stepNumber = 1,
            nombre = "Robinson Aravena",
            email = "rob.arav@gmail.com",
            password = "esurance",
            confirmarPassword = "esurance",
            nombreEstudio = "Noix Studio";

        var parameters = new RegisterStudioParameters(stepNumber, email, nombre, password, confirmarPassword, null, null, null, nombreEstudio, null);

        koViewModel = new koViewModelVar(parameters);
        ko.applyBindings(koViewModel);

        loadStep(1);

        $(document).on("change", "#ddlRegiones", changeDdlRegiones);
        $(document).on("change, blur", ".register-studio-form input", function () { validateForm($(this)); });
        $(document).on("click", ".register-studio-form .next-button input[value=Anterior]", loadPreviousStep);
        $(document).on("click", ".register-studio-form .next-button input[value=Siguiente]", loadNextStep);

        return koViewModel;
    },
    validateForm = function(validate) {
        var isValid = validate.valid();

        return isValid;
    },
    loadPreviousStep = function () {
        var numberStep = parseInt(koViewModel.PasoNumero());
        loadStep(numberStep - 1);
    },
    loadNextStep = function() {
        var numberStep = parseInt(koViewModel.PasoNumero());
        loadStep(numberStep + 1);
    },
    loadStep = function (stepNumber) {
        if ($(".register-studio-form form").length > 0 && !$(".register-studio-form form").valid()) return;

        var nextStep = stepNumber > koViewModel.PasoNumero();
        var direction1, direction2;

        if (koViewModel.PasoNumero() != null && koViewModel.PasoNumero() != stepNumber) {
            direction1 = nextStep ? "left" : "right";
            direction2 = nextStep ? "right" : "left";
        }
        
        koViewModel.PasoNumero(stepNumber);
        var model = ko.toJSON(koViewModel);

        $.ajax({
            url: "/User/LoadStepView",
            data: model,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'html',
            success: function (data) {
                if ($(".register-studio-form").html().trim() != "") {
                    $(".register-studio-form > div").hide('slide', { direction: direction1 }, 250, function () {
                        loadStepHtml(data, direction2);
                    });
                } else loadStepHtml(data);
            },
        });
    },
    loadStepHtml = function(data, direction) {
        $(".register-studio-form").html(data);
        $(".register-studio-form > div").hide();
        
        if (direction != null) $(".register-studio-form > div").show('slide', { direction: direction }, 250);
        else $(".register-studio-form > div").show();
        
        ko.applyBindings(koViewModel, $(".register-studio-form").children().get(0));
        reBindFormValidations();
    },
    changeDdlRegiones = function(data) {
        $.post("User/ComunasDeUnaRegion", $("#ddlRegiones").val(), function () {
            $("select[id$=Estudio_IdComuna] > option").remove();

            $.each(data, function (key, value) {
                $('#Estudio_IdComuna')
                    .append($("<option></option>")
                        .attr("value", key)
                        .text(value));
            });
        });
    },
    reBindFormValidations = function() {
        $(".register-studio-form form").removeData("validator");
        $(".register-studio-form form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(".register-studio-form form");
        
        $(".field-validation-error").html("");
        $(".field-validation-error").removeClass("field-validation-error").addClass("field-validation-valid");
        $(".register-studio-form form").validate().resetForm();
    },
    koViewModelVar = function(parameters) {
        this.PasoNumero = ko.observable(parameters.StepNumber);
        this.Usuario = {
            Email: ko.observable(parameters.Email),
            Nombre: ko.observable(parameters.Nombre),
            Password: ko.observable(parameters.Password),
            PasswordConfirmacion: ko.observable(parameters.ConfirmarPassword)
        };
        this.Estudio = {
            Email: ko.observable(parameters.EmailContacto),
            Telefono: ko.observable(parameters.Telefono),
            Celular: ko.observable(parameters.Celular),
            Nombre: ko.observable(parameters.NombreEstudio),
            SitioWeb: ko.observable(parameters.SitioWeb),
            UrlName: ko.observable(parameters.UrlName)
        };

        return this;
    },
    koViewModel;

    return {
        Init: init
    };
};