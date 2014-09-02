var RegisterStudioConfiguration = function (viewKoViewModel) {
    var init = function() {
        setViewModelData(viewKoViewModel);

        koViewModel = viewKoViewModel;
        ko.applyBindings(koViewModel);

        loadStep(4);

        $(document).on("click", ".register-studio-form .next-button input", changeStep);
        $(document).on("click", ".register-studio-form .new-room a", roomHelper.addNewRoom);
        $(document).on("click", ".register-studio-form .delete-room a", roomHelper.deleteRoom);
        $(document).on("click", ".register-studio-form .new-equipment a", equipmentHelper.addNewEquipment);
        $(document).on("click", ".register-studio-form .display-equipment .equipment-actions .ask-confirmation", equipmentHelper.askConfirmationForDeleteEquipment);
        $(document).on("click", ".register-studio-form .display-equipment .equipment-actions .delete-equipment", equipmentHelper.deleteEquipment);
        $(document).on("click", ".register-studio-form .display-equipment .equipment-actions .cancel-delete", equipmentHelper.cancelDeleteEquipment);
        $(document).on("click", ".register-studio-form .display-equipment .equipment-actions .edit-equipment", equipmentHelper.editEquipment);
        $(document).on("click", ".register-studio-form .equipment-header .disable-equipment-question a", equipmentHelper.askConfirmationForDisableAllEquipment);
        $(document).on("click", ".register-studio-form .equipment-header .cancel-disable", equipmentHelper.cancelDisableAllEquipment);
        $(document).on("click", ".register-studio-form .equipment-header .disable-equipments", equipmentHelper.disableEquipment);
        $(document).on("click", ".register-studio-form .equipment-header .enable-equipments", equipmentHelper.enableEquipment);
        $(document).on("change", "#ddlRegiones", changeDdlRegiones);
        $(document).on("change, blur", ".register-studio-form form input", function () { validateForm($(this)); });

        History.Adapter.bind(window, 'statechange', catchHistoryStateChange);
        cleanHistoryState();

        return koViewModel;
    },
    equipmentHelper = {
        addNewEquipment: function (event) {
            if (validateForm($(".register-studio-form form"))) {
                var element = $(event.srcElement);
                var index = $(".register-studio-room").index(element.parents(".register-studio-room"));

                koViewModel.Estudio.IndiceSeleccionado(index);
                koViewModel.Accion("AddNewEquipment");
                var model = ko.toJSON(koViewModel);

                $.postJSON("/User/EditEquipment", model, function (data) {
                    var htmlData = $(data);
                    updateViewModel(htmlData);

                    $(".register-studio-form").html(htmlData);
                    ko.applyBindings(koViewModel, $(".register-studio-form").children().get(0));

                    var studioRooms = cleanRoomBorderTop();

                    $(studioRooms[index]).find("form").last().hide();
                    $(studioRooms[index]).find("form").last().show("slide", { directio: "right" }, 350);

                    reBindFormValidations();
                });
            }

            return false;
        },
        enableEquipment: function (event) {
            var editButton = $(event.srcElement);
            var indexRoom = $(".register-studio-room").index(editButton.parents(".register-studio-room"));
            
            koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            koViewModel.Accion("EnableEquipment");
            var model = ko.toJSON(koViewModel);

            $.postJSON("/User/EditEquipment", model, function (data) {
                var htmlData = $(data);
                updateViewModel(htmlData);
                $(".register-studio-form").html(htmlData);

                cleanRoomBorderTop();
            });

            return false;
        },
        disableEquipment: function (event) {
            var editButton = $(event.srcElement);
            var indexRoom = $(".register-studio-room").index(editButton.parents(".register-studio-room"));
            
            koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            koViewModel.Accion("DisableEquipment");
            var model = ko.toJSON(koViewModel);

            $.postJSON("/User/EditEquipment", model, function (data) {
                var htmlData = $(data);
                updateViewModel(htmlData);
                $(".register-studio-form").html(htmlData);

                cleanRoomBorderTop();
            });

            return false;
        },
        editEquipment: function (event) {
            var editButton = $(event.srcElement);
            var indexEquipment = editButton.parents(".register-studio-room").find(".register-studio-equipment").index(editButton.parents(".register-studio-equipment"));
            var indexRoom = $(".register-studio-room").index(editButton.parents(".register-studio-room"));

            koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            koViewModel.Accion("EditEquipment");
            koViewModel.Estudio.Salas()[indexRoom].IndiceSeleccionado(indexEquipment);
            var model = ko.toJSON(koViewModel);

            $.postJSON("/User/EditEquipment", model, function (data) {
                var htmlData = $(data);
                updateViewModel(htmlData);
                $(".register-studio-form").html(htmlData);

                cleanRoomBorderTop();
            });

            return false;
        },
        deleteEquipment: function (event) {
            var deleteButton = $(event.srcElement);
            var indexEquipment = deleteButton.parents(".register-studio-room").find(".register-studio-equipment").index(deleteButton.parents(".register-studio-equipment"));
            var indexRoom = $(".register-studio-room").index(deleteButton.parents(".register-studio-room"));

            koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            koViewModel.Accion("DeleteEquipment");
            koViewModel.Estudio.Salas()[indexRoom].IndiceSeleccionado(indexEquipment);
            var model = ko.toJSON(koViewModel);

            $.postJSON("/User/EditEquipment", model, function (data) {
                deleteButton.parents(".register-studio-equipment").hide('slide', { direction: 'left' }, 350, function () {
                    var htmlData = $(data);
                    updateViewModel(htmlData);
                    $(".register-studio-form").html(htmlData);

                    cleanRoomBorderTop();
                });
            });

            return false;
        },
        cancelDeleteEquipment: function (event) {
            var deleteConfirmation = $(event.srcElement).parents(".delete-confirmation");
            var equipment = deleteConfirmation.parents(".register-studio-equipment");
            var deleteButton = equipment.find(".ask-confirmation");
        
            deleteConfirmation.hide('slide', { direction: 'right' }, 350, function() {
                deleteButton.show();
            });

            return false;
        },
        askConfirmationForDeleteEquipment: function(event) {
            var deleteButton = $(event.srcElement).parents(".ask-confirmation");
            var equipment = deleteButton.parents(".register-studio-equipment");
            var confirmation = equipment.find(".delete-confirmation");

            deleteButton.hide();
            confirmation.show('slide', { direction: 'right' }, 350);
            
            return false;
        },
        askConfirmationForDisableAllEquipment: function(event) {
            var disableButton = $(event.srcElement).parents(".disable-equipment-question");
            var equipment = disableButton.parents(".equipment-header");
            var confirmation = equipment.find(".disable-equipment-confirmation");

            disableButton.hide();
            confirmation.show('slide', { direction: 'left' }, 250);
            
            return false;
        },
        cancelDisableAllEquipment: function (event) {
            var disableConfirmation = $(event.srcElement).parents(".disable-equipment-confirmation");
            var equipment = disableConfirmation.parents(".equipment-header");
            var disableButton = equipment.find(".disable-equipment-question");

            disableConfirmation.hide('slide', { direction: 'left' }, 350, function () {
                disableButton.show();
            });

            return false;
        },
    },
    roomHelper = {
        addNewRoom: function () {
            if (validateForm($(".register-studio-form form"))) {
                var model = ko.toJSON(koViewModel);

                $.postJSON("/User/AddNewRoom", model, function (data) {
                    var htmlData = $(data);
                    updateViewModel(htmlData);
                    
                    $(".register-studio-form").html(htmlData);
                    ko.applyBindings(koViewModel, $(".register-studio-form").children().get(0));

                    var studioRooms = $(".form .register-studio-form .input-form .register-studio-room");
                    studioRooms.last().hide();
                    studioRooms.last().show("slide", { directio: "right" }, 350);
                    
                    reBindFormValidations();
                });
            }

            return false;
        },
        deleteRoom: function (event) {
            var deleteButton = $(event.srcElement);
            var index = $(".register-studio-room").index(deleteButton.parents(".register-studio-room"));
        
            koViewModel.Estudio.IndiceSeleccionado(index);
            var model = ko.toJSON(koViewModel);

            $.postJSON("/User/DeleteRoom", model, function (data) {
                deleteButton.parents(".register-studio-room").hide('slide', { direction: 'left' }, 350, function() {
                    var htmlData = $(data);
                    updateViewModel(htmlData);
                    $(".register-studio-form").html(htmlData);

                    cleanRoomBorderTop();
                });
            });

            return false;
        },
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
        var isValid = true;
        var errorsAlreadyDisplayed = $(".field-validation-error:visible");
        
        validate.each(function () { if (!$(this).valid()) isValid = false; });

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
        if (!validateForm($(".register-studio-form form"))) return;

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

        $.postJSON("/User/LoadStepView", model,  function (data) {
            if ($(".register-studio-form").html().trim() != "") {
                $(".register-studio-form > div").hide('slide', { direction: direction1 }, 250, function () {
                    loadStepHtml(data, direction2);
                });
            } else loadStepHtml(data);
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
    updateViewModel = function(data) {
        var model = data.find("#JsonModelResult").val();
        data.find("#JsonModelResult").remove();
        koViewModel = ko.mapping.fromJS(JSON.parse(model));
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
        //viewModel.Estudio.Salas()[0].Equipos()[0].Nombre("noix guitar");
    },
    koViewModel;

    return {
        Init: init
    };
};