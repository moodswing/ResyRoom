var RegisterStudioConfiguration = function (viewKoViewModel) {
    var init = function() {
        setViewModelData(viewKoViewModel);

        koViewModel = viewKoViewModel;
        ko.applyBindings(koViewModel);

        loadStep(3);

        $(document).on("change", "#ddlRegiones", changeDdlRegiones);
        $(document).on("change, blur", ".register-studio-form input", function () { validateForm($(this)); });
        $(document).on("click", ".register-studio-form .next-button input[value=Anterior]", loadPreviousStep);
        $(document).on("click", ".register-studio-form .next-button input[value=Siguiente]", loadNextStep);
        $(document).on("click", ".register-studio-form .new-room a", addNewRoom);

        return koViewModel;
    },
    addNewRoom = function() {
        if (validateForm($(".register-studio-form form"))) {
            var model = ko.toJSON(koViewModel);

            $.ajax({
                url: "/User/AddNewRoom",
                data: model,
                type: 'POST',
                contentType: 'application/json',
                dataType: 'html',
                success: function (data) {
                    $(".register-studio-form").html(data);

                    var studioRooms = $(".form .register-studio-form .input-form .register-studio-room");
                    studioRooms.removeClass("first-child");
                    studioRooms.first().addClass("first-child");
                    studioRooms.last().hide();
                    studioRooms.last().show("slide", { directio: "right" }, 350);
                },
            });
        }
    },
    validateForm = function (validate) {
        var errorsAlreadyDisplayed = $(".field-validation-error:visible");

        var isValid = validate.valid();

        if (isValid) {
            $(".field-info-description").show();
        } else {
            var errors = $(".field-validation-error:visible");

            $(".register-studio-form .value").not(errors.parents(".value")).find(".field-info-description:hidden").show('slide', { direction: 'left' }, 350);
            errors.parents(".value").find(".field-info-description").hide();

            errors.not(errorsAlreadyDisplayed).hide();
            errors.not(errorsAlreadyDisplayed).show('slide', { direction: 'left' }, 350);
        }

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
        if ($(".register-studio-form form").length > 0 && !validateForm($(".register-studio-form form"))) return;

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
    setViewModelData = function (viewModel) {
        viewModel.Usuario.Nombre("Robinson Aravena");
        viewModel.Usuario.Email("rob.arav@gmail.com");
        viewModel.Usuario.Password("esurance");
        viewModel.Usuario.PasswordConfirmacion("esurance");
        viewModel.Estudio.Nombre("Noix Studio");
        viewModel.Estudio.UrlName("noixStudio");
        viewModel.Estudio.Email("noix.studio@noix.cl");
        viewModel.Estudio.Direccion("noix street 123");
        viewModel.Estudio.Salas()[0].Nombre("noix 1");
    },
    koViewModel;

    return {
        Init: init
    };
};