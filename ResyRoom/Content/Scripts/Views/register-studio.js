var RegisterStudioConfiguration = function (viewKoViewModel) {
    var init = function() {
        setViewModelData(viewKoViewModel);

        koViewModel = viewKoViewModel;
        ko.applyBindings(koViewModel);

        loadStep(3);

        $(document).on("click", ".register-studio-form .next-button input", changeStep);
        $(document).on("click", ".register-studio-form .new-room a", addNewRoom);
        $(document).on("click", ".register-studio-form .new-equipment a", addNewEquipment);
        $(document).on("click", ".register-studio-form .delete-room a", deleteRoom);
        $(document).on("change", "#ddlRegiones", changeDdlRegiones);
        $(document).on("change, blur", ".register-studio-form form input", function () { validateForm($(this)); });

        History.Adapter.bind(window, 'statechange', catchHistoryStateChange);
        cleanHistoryState();

        return koViewModel;
    },
    addNewEquipment = function (event) {
        if (validateForm($(".register-studio-form form"))) {
            var element = $(event.srcElement);
            var index = $(".register-studio-room").index(element.parents(".register-studio-room"));

            koViewModel.AgregarEquipoSalaIndice(index);
            var model = ko.toJSON(koViewModel);

            $.ajax({
                url: "/User/AddNewEquipment",
                data: model,
                type: 'POST',
                contentType: 'application/json',
                dataType: 'html',
                success: function (data) {
                    koViewModel.Estudio.Salas()[index].Equipos().push({
                        Nombre: ko.observable("noix equipment"),
                        Descripcion: ko.observable(null),
                        TieneUnPrecioAdicional: ko.observable(false),
                        PrecioAdicional: ko.observable(null),
                        Indice: ko.observable(null),
                        Fotografia: ko.observable(null)
                    });
                    
                    $(".register-studio-form").html(data);
                    ko.applyBindings(koViewModel, $(".register-studio-form").children().get(0));

                    var studioRooms = cleanRoomBorderTop();

                    $(studioRooms[index]).find("form").last().hide();
                    $(studioRooms[index]).find("form").last().show("slide", { directio: "right" }, 350);

                    reBindFormValidations();
                },
            });
        }

        return false;
    },
    deleteRoom = function (event) {
        var element = $(event.srcElement);
        var index = $(".register-studio-room").index(element.parents(".register-studio-room"));
        
        koViewModel.EliminaSalaIndice(index);
        var model = ko.toJSON(koViewModel);

        $.ajax({
            url: "/User/DeleteRoom",
            data: model,
            type: 'POST',
            contentType: 'application/json',
            dataType: 'html',
            success: function (data) {
                element.parents(".register-studio-room").hide('slide', { direction: 'left' }, 350, function() {
                    element.parents(".register-studio-room").remove();

                    $(".register-studio-form").html(data);
                    koViewModel.Estudio.Salas.remove(koViewModel.Estudio.Salas()[index]);

                    cleanRoomBorderTop();
                });
            },
        });

        return false;
    },
    addNewRoom = function () {
        if (validateForm($(".register-studio-form form"))) {
            var model = ko.toJSON(koViewModel);

            $.ajax({
                url: "/User/AddNewRoom",
                data: model,
                type: 'POST',
                contentType: 'application/json',
                dataType: 'html',
                success: function (data) {
                    model = $(data).find("#JsonModelResult").val();
                    koViewModel = ko.mapping.fromJS(JSON.parse(model));
                    
                    //koViewModel.Estudio.Salas().push({
                    //    Nombre: ko.observable("prueba"),
                    //    Tamaño: ko.observable(null),
                    //    SetDePlatos: ko.observable(null),
                    //    Equipos: ko.observable([{
                    //        Nombre: ko.observable("equipo1"),
                    //        Descripcion: ko.observable(null),
                    //        TieneUnPrecioAdicional: ko.observable(false),
                    //        PrecioAdicional: ko.observable(null),
                    //        Indice: ko.observable(null),
                    //        Fotografia: ko.observable(null),
                    //        ViewState: ko.observable(2)
                    //    }])
                    //});

                    $(".register-studio-form").html(data);
                    ko.applyBindings(koViewModel, $(".register-studio-form").children().get(0));

                    var studioRooms = cleanRoomBorderTop();
                    
                    studioRooms.last().hide();
                    studioRooms.last().show("slide", { directio: "right" }, 350);
                    
                    reBindFormValidations();
                },
            });
        }

        return false;
    },
    cleanRoomBorderTop = function() {
        var studioRooms = $(".form .register-studio-form .input-form .register-studio-room");
        studioRooms.removeClass("first-child");
        studioRooms.first().addClass("first-child");

        var nameRooms = $(".form .register-studio-form .input-form .room-name");
        nameRooms.removeClass("first-child");
        nameRooms.first().addClass("first-child");

        return studioRooms;
    },
    catchHistoryStateChange = function() {
        var state = History.getState();

        if (state.data != null && state.data.StepNumber != null)
            loadStep(state.data.StepNumber);
    },
    validateForm = function (validate) {
        var errorsAlreadyDisplayed = $(".field-validation-error:visible");

        var isValid = validate.valid();
        if (isValid)
            $(".field-info-description").show();
        else {
            var errors = $(".field-validation-error:visible");

            $(".register-studio-form .value").not(errors.parents(".value")).find(".field-info-description:hidden").show('slide', { direction: 'left' }, 350);
            errors.parents(".value").find(".field-info-description").hide();

            errors.not(errorsAlreadyDisplayed).hide();
            errors.not(errorsAlreadyDisplayed).show('slide', { direction: 'left' }, 350);
        }

        return isValid;
    },
    changeStep = function (event) {
        if ($(".register-studio-form form").length > 0) {
            var isValid = true;
            $(".register-studio-form form").each(function() {
                if (!validateForm($(this))) isValid = false;
            });
        }

        if (!isValid) return;

        var stepNumber;
        if ($(event.srcElement).is("[value=Anterior]")) stepNumber  = koViewModel.PasoNumero() - 1;
        else stepNumber = koViewModel.PasoNumero() + 1;

        History.pushState({ StepNumber: stepNumber }, 'ResyRoom - Registra tu estudio: Paso ' + stepNumber, null);
    },
    loadStep = function (stepNumber) {
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

        cleanRoomBorderTop();
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
        if ($(".register-studio-form form").length > 0) $(".register-studio-form form").validate().resetForm();
    },
    cleanHistoryState = function() {
        History.replaceState({ randomData: window.Math.random() }, '', null);
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
        viewModel.Estudio.Salas()[0].Equipos()[0].Nombre("noix guitar");
    },
    koViewModel;

    return {
        Init: init
    };
};