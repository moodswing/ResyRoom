$(document).ready(function() {

    $("form").each(function() {
        $(this).bind("invalid-form.validate", function (evt, validator) {
            var validMessages = [];
            $.each(validator.errorList, function() {
                if (this.message.indexOf('field is required') == -1)
                    validMessages.push(this.message);
            });

            if (validMessages.length > 0) {
                myViewModel.MostrarNotificaciones(true);
                var mensajes = [];

                $.each(validMessages, function() {
                    mensajes.push(new messageNotificacion(this));
                });

                myViewModel.Notificaciones(new notificacion("error", mensajes));
            } else {
                myViewModel.MostrarNotificaciones(false);
            }
        });
    });

});