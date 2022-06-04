$.validator.unobtrusive.adapters.add('requirediftextboxhasvalue', ['dependentproperty'], function (options) {
    options.rules['requirediftextboxhasvalue'] = options.params;
    options.messages['requirediftextboxhasvalue'] = options.message;
});


$(document).ready(function () {

    $.validator.addMethod('requirediftextboxhasvalue', function (value, element, parameters) {
        //var desiredvalue = parameters.desiredvalue;
        //desiredvalue = (desiredvalue == null ? '' : desiredvalue).toString();
        //var controlType = $("input[id$='" + parameters.dependentproperty + "']").attr("type");
        actualvalue = $("#" + parameters.dependentproperty).val();
        if ($.trim(actualvalue).length > 0) {
            var isValid = $.validator.methods.required.call(this, value, element, parameters);
            return isValid;
        }
        return true;
    });
});