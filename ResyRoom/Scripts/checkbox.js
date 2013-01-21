$("div.checkbox :checkbox").live("change", function () {
    //    if (!$(this).parent().hasClass("checkbox")) return;
    if (this.checked) {
        $(this).parent().removeClass("unchecked").addClass("checked");
    }
    else {
        $(this).parent().removeClass("checked").addClass("unchecked");
    }
});