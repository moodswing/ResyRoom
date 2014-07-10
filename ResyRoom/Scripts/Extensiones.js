String.prototype.toMoney = function () {
    var cnt = this;
    cnt = cnt.toString().replace(/\$|\,/g, '');
    if (isNaN(cnt))
        return 0;
    var sgn = (cnt == (cnt = Math.abs(cnt)));
    cnt = Math.floor(cnt * 100 + 0.5);

    cnt = Math.floor(cnt / 100).toString();

    for (var i = 0; i < Math.floor((cnt.length - (1 + i)) / 3); i++)
        cnt = cnt.substring(0, cnt.length - (4 * i + 3)) + '.' + cnt.substring(cnt.length - (4 * i + 3));
    return (((sgn) ? '' : '-') + cnt);
};

$.fn.llenaDropDownListHijo = function (accion, ddlHijo) {
    var idDdl = this.val();
    if (idDdl != null && idDdl != '') {
        $.getJSON(accion, { idItem: idDdl }, function (items) {
            ddlHijo = $(ddlHijo);
            ddlHijo.empty();
            $.each(items, function (index, comuna) {
                ddlHijo.append($('<option/>', {
                    value: comuna.Value,
                    text: comuna.Text
                }));
            });
        });
    }
};

$.fn.preload = function () {
    this.each(function () {
        $('<img/>')[0].src = this;
    });
};