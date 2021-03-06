﻿var RegisterStudioConfiguration = function (vmkoViewModel) {
    var init = function() {
        vm = vmkoViewModel;
        SetDataTesting(vm).RegisterEstudio();

        ko.applyBindings(vm.koViewModel);
        
        loadStep(1);

        setDocumentEvents();
        cleanHistoryState();
    },
    setDocumentEvents = function() {
        $(document).on("click", ".register-studio-form .next-button input[value!=Guardar]", changeStep);
        $(document).on("click", ".register-studio-form .next-button input[value=Guardar]", saveStudio);
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
        $(document).on("change", "#Estudio_IdRegion", changeDdlRegiones);
        $(document).on("change, blur", ".register-studio-form form input", function () { validateForm($(this)); });

        History.Adapter.bind(window, 'statechange', catchHistoryStateChange);
    },
    setJqueryPlugins = function() {
        $("[name=HoraApertura]").timeEntry({ ampmPrefix: ' ', useMouseWheel: true, separator: ' : ', timeSteps: [1, 15, 0], minTime: new Date(0, 0, 0, 7, 30, 0), maxTime: new Date(0, 0, 0, 2, 0, 0) });
        $("[name=HoraCierre]").timeEntry({ ampmPrefix: ' ', useMouseWheel: true, separator: ' : ', timeSteps: [1, 15, 0], minTime: new Date(0, 0, 0, 7, 30, 0), maxTime: new Date(0, 0, 0, 2, 0, 0) });
        $("[name=DuracionBloque]").timeEntry({ show24Hours: true, useMouseWheel: true, separator: ' : ', timeSteps: [1, 15, 0], minTime: new Date(0, 0, 0, 0, 15, 0), maxTime: new Date(0, 0, 0, 5, 0, 0) });
    },
    setInputMasks = function() {
        $("#Estudio_UrlName").mask('LL', { translation: { 'L': { pattern: /[a-zA-Z0-9_]/, recursive: true } }, maxlength: false });
        $("#Estudio_Telefono").mask('NN', { translation: { 'N': { pattern: /[0-9- +]/, recursive: true } }, maxlength: false });
        $("#Estudio_Celular").mask('NN', { translation: { 'N': { pattern: /[0-9- +]/, recursive: true } }, maxlength: false });
        $("[name=PrecioAdicional]").mask('##', { reverse: true, maxlength: false });
        $("[name=PrecioPorBloque]").mask('##', { reverse: true, maxlength: false });
    },
    saveStudio = function() {
        if (validateForm($(".register-studio-form form"))) {
            $.ajax({
                type: "POST",
                url: "/User/SaveStudio",
                data: getModel(),
                dataType: 'json',
                contentType: 'application/json',
                crossDomain: true,
                success: function (data) {
                    window.location.href = data;
                }
            });
        }

        return false;
    },
    equipmentHelper = {
        addNewEquipment: function (event) {
            if (!validateForm($(".register-studio-form form"))) return false;
            
            var element = $(event.srcElement);
            var index = $(".register-studio-room").index(element.parents(".register-studio-room"));

            vm.koViewModel.Estudio.IndiceSeleccionado(index);
            vm.koViewModel.Accion("AddNewEquipment");
                
            $.postJSON("/User/EditEquipment", getModel(), function (data) {
                var htmlData = $(data);
                updateViewModel(htmlData);

                $(".register-studio-form").html(htmlData);
                rebindViewModel($(".register-studio-form").children().get(0));

                var studioRooms = cleanPage();

                $(studioRooms[index]).find("form").last().hide();
                $(studioRooms[index]).find("form").last().show("slide", { direction: "right" }, 350);

                setInputMasks();
                reBindFormValidations();
            });

            return false;
        },
        enableEquipment: function(event) {
            var editButton = $(event.srcElement);
            var indexRoom = $(".register-studio-room").index(editButton.parents(".register-studio-room"));

            vm.koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            vm.koViewModel.Accion("EnableEquipment");
            
            $.postJSON("/User/EditEquipment", getModel(), function (data) {
                var htmlData = $(data);
                updateViewModel(htmlData);
                $(".register-studio-form").html(htmlData);

                cleanPage();
            });

            return false;
        },
        disableEquipment: function(event) {
            var editButton = $(event.srcElement);
            var indexRoom = $(".register-studio-room").index(editButton.parents(".register-studio-room"));

            vm.koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            vm.koViewModel.Accion("DisableEquipment");
            
            $.postJSON("/User/EditEquipment", getModel(), function (data) {
                var htmlData = $(data);
                updateViewModel(htmlData);
                $(".register-studio-form").html(htmlData);

                cleanPage();
            });

            return false;
        },
        editEquipment: function(event) {
            if (!validateForm($(".register-studio-form form"))) return false;

            var editButton = $(event.srcElement);
            var indexEquipment = editButton.parents(".register-studio-room").find(".register-studio-equipment").index(editButton.parents(".register-studio-equipment"));
            var indexRoom = $(".register-studio-room").index(editButton.parents(".register-studio-room"));

            vm.koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            vm.koViewModel.Accion("EditEquipment");
            vm.koViewModel.Estudio.Salas()[indexRoom].IndiceSeleccionado(indexEquipment);
            
            $.postJSON("/User/EditEquipment", getModel(), function (data) {
                var htmlData = $(data);
                updateViewModel(htmlData);
                $(".register-studio-form").html(htmlData);

                cleanPage();
                rebindViewModel($(".register-studio-form").children().get(0));
                reBindFormValidations();
            });

            return false;
        },
        deleteEquipment: function(event) {
            var deleteButton = $(event.srcElement);
            var indexEquipment = deleteButton.parents(".register-studio-room").find(".register-studio-equipment").index(deleteButton.parents(".register-studio-equipment"));
            var indexRoom = $(".register-studio-room").index(deleteButton.parents(".register-studio-room"));

            vm.koViewModel.Estudio.IndiceSeleccionado(indexRoom);
            vm.koViewModel.Accion("DeleteEquipment");
            vm.koViewModel.Estudio.Salas()[indexRoom].IndiceSeleccionado(indexEquipment);
            
            $.postJSON("/User/EditEquipment", getModel(), function (data) {
                deleteButton.parents(".register-studio-equipment").hide('slide', { direction: 'left' }, 350, function() {
                    var htmlData = $(data);
                    updateViewModel(htmlData);
                    $(".register-studio-form").html(htmlData);

                    cleanPage();
                    rebindViewModel($(".register-studio-form").children().get(0));
                    reBindFormValidations();
                });
            });

            return false;
        },
        cancelDeleteEquipment: function(event) {
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
        cancelDisableAllEquipment: function(event) {
            var disableConfirmation = $(event.srcElement).parents(".disable-equipment-confirmation");
            var equipment = disableConfirmation.parents(".equipment-header");
            var disableButton = equipment.find(".disable-equipment-question");

            disableConfirmation.hide('slide', { direction: 'left' }, 350, function() {
                disableButton.show();
            });

            return false;
        },
    },
    roomHelper = {
        addNewRoom: function() {
            if (validateForm($(".register-studio-form form"))) {
                vm.koViewModel.Accion("AddNewRoom");
                
                $.postJSON("/User/EditRoom", getModel(), function (data) {
                    var htmlData = $(data);
                    updateViewModel(htmlData);

                    $(".register-studio-form").html(htmlData);
                    rebindViewModel($(".register-studio-form").children().get(0));

                    var studioRooms = cleanPage();
                    studioRooms.last().hide();
                    studioRooms.last().show("slide", { directio: "right" }, 350);

                    reBindFormValidations();
                });
            }

            return false;
        },
        deleteRoom: function(event) {
            var deleteButton = $(event.srcElement);
            var index = $(".register-studio-room").index(deleteButton.parents(".register-studio-room"));

            vm.koViewModel.Accion("DeleteRoom");
            vm.koViewModel.Estudio.IndiceSeleccionado(index);
            
            $.postJSON("/User/EditRoom", getModel(), function (data) {
                deleteButton.parents(".register-studio-room").hide('slide', { direction: 'left' }, 350, function() {
                    var htmlData = $(data);
                    updateViewModel(htmlData);
                    $(".register-studio-form").html(htmlData);

                    cleanPage();
                });
            });

            return false;
        },
    },
    getModel = function() {
        var model = {
            Usuario: vm.koViewModel.Usuario,
            Estudio: vm.koViewModel.Estudio,
            Accion: vm.koViewModel.Accion,
            PasoNumero: vm.koViewModel.PasoNumero,
            PasoActual: vm.koViewModel.PasoActual
        };
        
        for (var i = 0; i < model.Estudio.Salas().length; i++) {
            var sala = model.Estudio.Salas()[i];

            sala.EstudiosMasPopulares = null;
            sala.EstudiosMejorEvaluados = null;
            sala.Horarios = null;
        }

        return ko.toJSON(model);
    },
    cleanPage = function() {
        var studioRooms = $(".form .register-studio-form .input-form .register-studio-room");
        studioRooms.removeClass("first-child");
        studioRooms.first().addClass("first-child");

        var nameRooms = $(".form .register-studio-form .input-form .room-name");
        nameRooms.removeClass("first-child");
        nameRooms.first().addClass("first-child");

        $(".delete-room-link").first().remove();

        return studioRooms;
    },
    addExtraValidations = function () {
        if (ExtraValidationsConfiguration != null) {
            ExtraValidationsConfiguration().SetRequiredIfValidation();
        }
    },
    catchHistoryStateChange = function() {
        var state = History.getState();

        if (state.data != null && state.data.StepNumber != null)
            loadStep(state.data.StepNumber);
    },
    validateForm = function(validate) {
        var isValid = true;
        var errorsAlreadyDisplayed = $(".field-validation-error:visible");

        validate.each(function () {
            if ($(this).is("[type=checkbox]")) return;
            if (!$(this).valid()) isValid = false;
        });

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
    changeStep = function(event) {
        if (!validateForm($(".register-studio-form form"))) return;

        vm.koViewModel.PasoActual(vm.koViewModel.PasoNumero());

        var stepNumber;
        if ($(event.srcElement).is("[value=Anterior]")) stepNumber = vm.koViewModel.PasoNumero() - 1;
        else stepNumber = vm.koViewModel.PasoNumero() + 1;

        cleanHistoryState();
        History.pushState({ StepNumber: stepNumber }, 'ResyRoom - Registra tu estudio: Paso ' + stepNumber, null);
    },
    loadStep = function(stepNumber) {
        var nextStep = stepNumber > vm.koViewModel.PasoNumero();
        var direction1, direction2;

        if (vm.koViewModel.PasoNumero() != null && vm.koViewModel.PasoNumero() != stepNumber) {
            direction1 = nextStep ? "left" : "right";
            direction2 = nextStep ? "right" : "left";
        }

        vm.koViewModel.PasoNumero(stepNumber);
        
        $.postJSON("/User/LoadStepView", getModel(), function (data) {
            if ($(".register-studio-form").html().trim() != "" && $(data).find(".field-validation-error").length == 0) {
                $(".register-studio-form > div").hide('slide', { direction: direction1 }, 250, function() {
                    loadStepHtml(data, direction2);
                });
            } else loadStepHtml(data);
        });
    },
    loadStepHtml = function(data, direction) {
        var htmlData = $(data);
        var idComuna = vm.koViewModel.Estudio.IdComuna();

        updateViewModel(htmlData);

        $(".register-studio-form").html(htmlData);
        $(".register-studio-form > div").hide();

        if (direction != null) $(".register-studio-form > div").show('slide', { direction: direction }, 250, function () {
            if ($("#Estudio_IdComuna").length > 0)
                $("#Estudio_IdComuna").val(idComuna);
        });
        else $(".register-studio-form > div").show();
        
        rebindViewModel($(".register-studio-form").children().get(0));

        setJqueryPlugins();
        setInputMasks();
        cleanPage();
        
        reBindFormValidations();
    },
    changeDdlRegiones = function () {
        var idRegion = $("#Estudio_IdRegion").val();
        if (idRegion == 0) {
            $("select[id$=Estudio_IdComuna] > option").remove();
            $('#Estudio_IdComuna')
                    .append($("<option></option>")
                        .attr("value", "0")
                        .text("Seleccione región"));
            
            return;
        }
        
        $.post("/User/ComunasDeUnaRegion", { id: idRegion }, function (data) {
            $("select[id$=Estudio_IdComuna] > option").remove();

            $.each(data, function(key, value) {
                $('#Estudio_IdComuna')
                    .append($("<option></option>")
                        .attr("value", value.IdComuna)
                        .text(value.Descripcion));
            });
        });
    },
    reBindFormValidations = function() {
        $(".register-studio-form form").removeData("validator");
        $(".register-studio-form form").removeData("unobtrusiveValidation");
        $.validator.unobtrusive.parse(".register-studio-form form");
        
        addExtraValidations();

        //var limpiaErrores = $(".field-validation-error");

        for (var i = 0; i < serverValidations.length; i++) {
            //limpiaErrores = limpiaErrores.not("[data-valmsg-for='" + serverValidations[i] + "']");
            $(".field-validation-error[data-valmsg-for='" + serverValidations[i] + "']").next(".field-info-description").hide();
            $("[data-valmsg-for='" + serverValidations[i] + "']").hide();
            $("[data-valmsg-for='" + serverValidations[i] + "']").show('slide', { direction: 'left' }, 350);
        }
        
        //limpiaErrores.html("");
        //limpiaErrores.removeClass("field-validation-error").addClass("field-validation-valid");
        //if ($(".register-studio-form form").length > 0) $(".register-studio-form form").validate().resetForm();
    },
    cleanHistoryState = function() {
        History.replaceState({ randomData: window.Math.random() }, '', null);
    },
    updateViewModel = function(data) {
        var model = data.find("#JsonModelResult").val();
        data.find("#JsonModelResult").remove();

        vm.koViewModel = ko.mapping.fromJS(JSON.parse(model));
    },
    rebindViewModel = function(container) {
        setExtraBindViewModel();
        ko.applyBindings(vm.koViewModel, container);
    },
    setExtraBindViewModel = function () {
        if ($(".new-schedule").length == 0) return;

        var setHourTime = function(time) {
            if (time.indexOf("PM") > -1) {
                var hour = time.split(":")[0];
                time = time.replace(hour, parseInt(hour) + 12);
            }
            time = time.split(" ").join("").split("AM").join("").split("PM").join("");

            return time;
        };
        
        for (var i = 0; i < vm.koViewModel.Estudio.Salas().length; i++) {
            var horario = vm.koViewModel.Estudio.Salas()[i].Horario;
            horario.DiaLunes = ko.observable(horario.DiasAbierto().indexOf("1") > -1);
            horario.DiaMartes = ko.observable(horario.DiasAbierto().indexOf("2") > -1);
            horario.DiaMiercoles = ko.observable(horario.DiasAbierto().indexOf("3") > -1);
            horario.DiaJueves = ko.observable(horario.DiasAbierto().indexOf("4") > -1);
            horario.DiaViernes = ko.observable(horario.DiasAbierto().indexOf("5") > -1);
            horario.DiaSabado = ko.observable(horario.DiasAbierto().indexOf("6") > -1);
            horario.DiaDomingo = ko.observable(horario.DiasAbierto().indexOf("7") > -1);

            horario.DiasAbierto = ko.computed(function () {
                var value = "";
                value = value + (this.DiaLunes() ? "1" : "");
                value = value + (this.DiaMartes() ? "2" : "");
                value = value + (this.DiaMiercoles() ? "3" : "");
                value = value + (this.DiaJueves() ? "4" : "");
                value = value + (this.DiaViernes() ? "5" : "");
                value = value + (this.DiaSabado() ? "6" : "");
                value = value + (this.DiaDomingo() ? "7" : "");

                return value;
            }, horario);

            var horaApertura = $($(".new-schedule")[i]).find("#HoraApertura").val();
            var horaCierre = $($(".new-schedule")[i]).find("#HoraCierre").val();
            var duracionBloque = $($(".new-schedule")[i]).find("#DuracionBloque").val();

            horario.HoraAperturaDisplay = ko.observable(horaApertura.indexOf(" : ") == -1 ? horaApertura.split(":").join(" : ") : horaApertura);
            horario.HoraCierreDisplay = ko.observable(horaCierre.indexOf(" : ") == -1 ? horaCierre.split(":").join(" : ") : horaCierre);
            horario.DuracionBloqueDisplay = ko.observable(duracionBloque.indexOf(" : ") == -1 ? duracionBloque.split(":").join(" : ") : duracionBloque);
            
            horario.HoraApertura = ko.computed(function () { return setHourTime(this.HoraAperturaDisplay()); }, horario);
            horario.HoraCierre = ko.computed(function () { return setHourTime(this.HoraCierreDisplay()); }, horario);
            horario.DuracionBloque = ko.computed(function () { return this.DuracionBloqueDisplay().split(" ").join(""); }, horario);
        }
    },
    serverValidations = ["Usuario.Email", "Estudio.Nombre", "Estudio.UrlName"],
    vm;

    return {
        Init: init
    };
};