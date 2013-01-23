$("div.checkbox :checkbox").live("change", function () {
    if ($("#" + this.id).parent().hasClass("uncheckedmini") || $("#" + this.id).parent().hasClass("checkedmini")) {
        if (this.checked) {
            $(this).parent().removeClass("uncheckedmini").addClass("checkedmini");
        }
        else {
            $(this).parent().removeClass("checkedmini").addClass("uncheckedmini");
        }
    }
    else {
        if (this.checked) {
            $(this).parent().removeClass("unchecked").addClass("checked");
        }
        else {
            $(this).parent().removeClass("checked").addClass("unchecked");
        }
    }
});