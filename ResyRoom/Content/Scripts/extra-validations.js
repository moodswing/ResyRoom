var ExtraValidationsConfiguration = function() {
    var requiredIf = function() {
        $.validator.addMethod('requiredif',
            function (value, element, parameters) {
                var id = '#' + parameters['dependentproperty'];

                var targetvalue = parameters['targetvalue'];
                targetvalue = (targetvalue == null ? '' : targetvalue).toString();

                var control = $(id);
                var controltype = control.attr('type');
                var actualvalue = controltype === 'checkbox' ? control.is(':checked').toString() : control.val();

                if (targetvalue === actualvalue)
                    return $.validator.methods.required.call(this, value, element, parameters);

                return true;
            }
        );

        $.validator.unobtrusive.adapters.add('requiredif', ['dependentproperty', 'targetvalue'], function (options) {
            options.rules['requiredif'] = {
                dependentproperty: options.params['dependentproperty'],
                targetvalue: options.params['targetvalue']
            };
            options.messages['requiredif'] = options.message;
        });
    };

    return {
        SetRequiredIfValidation: requiredIf
    };
};