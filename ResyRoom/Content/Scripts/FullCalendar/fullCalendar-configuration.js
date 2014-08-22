var CalendarParameters = function(calendarSelector, closedDays, slotDuration, minTime, maxTime, idSala, idEstudio, koViewModel) {
    return {
        CalendarSelector: calendarSelector,
        ClosedDays: closedDays,
        SlotDuration: slotDuration,
        MinTime: minTime,
        MaxTime: maxTime,
        IdSala: idSala,
        IdEstudio: idEstudio,
        KoViewModel: koViewModel
    };
};

var SetCalendarConfiguration = function (parameters) {
    function toHourMinuteFormat(duration) {
        return moment().subtract(moment.duration(moment().format("HH:mm"))).add(duration).format("HH:mm");
    }
    
    parameters.CalendarSelector.fullCalendar({
        header: { left: 'month,agendaWeek,agendaDay', center: 'title', right: 'prev,next' },
        monthNames: ['Enero', 'Febrero', 'Marzo', 'Abril', 'Mayo', 'Junio', 'Julio', 'Agosto', 'Septiembre', 'Octubre', 'Noviembre', 'Diciembre'],
        dayNames: ['Domingo', 'Lunes', 'Martes', 'Miercoles', 'Jueves', 'Viernes', 'Sábado'],
        dayNamesShort: ['Dom', 'Lun', 'Mar', 'Mie', 'Jue', 'Vie', 'Sáb'],
        buttonText: { today: "Hoy", month: "Mes", week: "Semana", day: "Día" },
        closedDays: parameters.ClosedDays,
        columnFormat: { month: 'ddd', week: 'ddd D/M', day: 'dddd D/M' },
        firstDay: 1,
        selectable: { agendaDay: true, agendaWeek: true, 'default': false, },
        defaultView: 'month',
        allDaySlot: { agendaWeek: false, 'default': true },
        weekMode: 5,
        firstHour: 10,
        allDayDefault: false,
        lazyFetching: true,
        slotDuration: parameters.SlotDuration,
        minTime: parameters.MinTime,
        maxTime: parameters.MaxTime,
        dayRender: function(date, cell) {
            if (moment(date.format("YYYY-MM-DD 00:00:00")) < moment(moment(new Date()).format("YYYY-MM-DD 00:00:00"))) 
            {
                cell.addClass("fc-past-day");
                return false;
            }
                    
            var sala = ko.utils.arrayFirst(parameters.KoViewModel.Salas(), function(item) { return item.IdSala == parameters.IdSala; });
            if (parameters.ClosedDays.indexOf(date.day()) != -1) {
                cell.addClass("fc-inactive");
                cell.find(".fc-day-content > div").text("No disponible");
            }
            else {
                var tieneReservas = false;
                for(var i = 0; i < sala.ReservasPorDia.length; i ++) {
                    var dailySlots = (moment.duration(sala.HoraCierre)._milliseconds - moment.duration(sala.HoraApertura)._milliseconds) / moment.duration(sala.DuracionBloque)._milliseconds;

                    var fechaFormatted = moment(sala.ReservasPorDia[i].Dia).format("YYYY-MM-DD");

                    if (date.format("YYYY-MM-DD") == fechaFormatted) {
                        var slotsDisponibles = dailySlots - sala.ReservasPorDia[i].NroReservas;
                        cell.find(".fc-day-content > div").text(slotsDisponibles + " espacios disponibles");
                        tieneReservas = true;

                        if (slotsDisponibles == 0) {
                            cell.find(".fc-day-content > div").text("Sin espacios disponibles");
                            cell.addClass("fc-inactive");
                        }
                    }
                }

                if (!tieneReservas) {
                    cell.find(".fc-day-content > div").text("Todo el día disponible");
                }
            }
        },
        events: function(start, end, timezone, callback) {
            var sala = ko.utils.arrayFirst(parameters.KoViewModel.Salas(), function(item) { return item.IdSala == parameters.IdSala; });
                    
            var events = [];
            for(var i = 0; i < sala.Reservas().length; i ++) {
                var desde = toHourMinuteFormat(moment.duration(sala.Reservas()[i].Desde));
                var hasta = toHourMinuteFormat(moment.duration(sala.Reservas()[i].Hasta));
                        
                if ((moment.duration(hasta) - moment.duration(desde)) > moment.duration(sala.DuracionBloque)) {
                    var a = moment.duration(desde), b = moment.duration(hasta), bloque = moment.duration(sala.DuracionBloque);
                    if (moment(sala.Reservas()[i].Dia + " " + toHourMinuteFormat(a)) < moment(new Date())) continue;

                    while (a < b) {
                        events.push({
                            title: "Reservado",
                            start: moment(sala.Reservas()[i].Dia).format("YYYY-MM-DD") + " " + toHourMinuteFormat(a),
                            end: moment(sala.Reservas()[i].Dia).format("YYYY-MM-DD") + " " + toHourMinuteFormat(moment.duration(a + bloque)),
                        });
                            
                        a = moment.duration(a + bloque);
                    }
                }
                else {
                    if (moment(sala.Reservas()[i].Dia + " " + desde) < moment(new Date())) continue;

                    events.push({
                        title: "Reservado",
                        start: moment(sala.Reservas()[i].Dia).format("YYYY-MM-DD") + " " + desde,
                        end: moment(sala.Reservas()[i].Dia).format("YYYY-MM-DD") + " " + hasta,
                    });
                }
            }
                    
            callback(events);
        },
        eventRender: function(event, element, view) {
            if (view.name == "month")
                return false;
                    
            element.find(".fc-event-time").remove();
            element.find(".fc-event-inner").addClass("div-table");
            element.find(".fc-event-title").addClass("div-cell");
            element.find(".fc-event-title").css("vertical-align", "middle");
        },
        eventColor: '#EFEFEF',
        eventTextColor: '#5D5D5D',
        select: function(start, end, jsEvent, view) {
            parameters.KoViewModel.HoraReservaDesde(start);
            parameters.KoViewModel.HoraReservaHasta(end);
            parameters.KoViewModel.IdSalaReserva(parameters.IdSala);
            parameters.KoViewModel.IdEstudioReserva(parameters.IdEstudio);

            if (parameters.ClosedDays.indexOf(start.day()) != -1 || moment(start.format("YYYY-MM-DD hh:mm:ss a")) < moment(moment(new Date()).format("YYYY-MM-DD hh:mm:ss a"))) {
                parameters.KoViewModel.HoraReservaDesde(null);
                parameters.KoViewModel.HoraReservaHasta(null);
                parameters.KoViewModel.IdSalaReserva(null);
            }

            if (moment(start.format("YYYY-MM-DD hh:mm:ss a")) < moment(moment(new Date()).format("YYYY-MM-DD hh:mm:ss a"))) {
                if (!parameters.KoViewModel.MostrarNotificacion()) {
                    parameters.KoViewModel.MostrarNotificacion(true);

                    var notificaciones = [new notificacion(2, ["La fecha y hora que desea reservar es de una fecha anterior a la actual, favor volver a seleccionar.."])]; // 2: error
                    parameters.KoViewModel.Notificaciones(notificaciones);
                }
            }
            else {
                parameters.KoViewModel.MostrarNotificacion(false);
            }
        },
        unselectAuto: false,
        viewRender: function(view, element) {
            var sala, fecha, reservas, reservasPorDia, dailySlots, slotsDisponibles;

            parameters.KoViewModel.HoraReservaDesde(null);
            parameters.KoViewModel.HoraReservaHasta(null);
            parameters.KoViewModel.IdSalaReserva(null);
                        
            if ((view.name == "agendaDay" && parameters.KoViewModel.FromCalendar()) || view.name == "agendaWeek") {
                sala = ko.utils.arrayFirst(parameters.KoViewModel.Salas(), function (item) { return item.IdSala == parameters.IdSala; });
                fecha = $("#calendar" + parameters.IdSala).fullCalendar("getDate").format("YYYY-MM-DD");
                reservas = ko.utils.arrayFilter(sala.Reservas(), function(item) { return moment(item.Dia).format("YYYY-MM-DD") == fecha; });
                reservasPorDia = ko.utils.arrayFilter(sala.ReservasPorDia, function(item) { return moment(item.Dia).format("YYYY-MM-DD") == fecha; });
                dailySlots = (moment.duration(sala.HoraCierre)._milliseconds - moment.duration(sala.HoraApertura)._milliseconds) / moment.duration(sala.DuracionBloque)._milliseconds;
                slotsDisponibles = dailySlots - (reservasPorDia.length > 0 ? reservasPorDia[0].NroReservas : 0);
            }

            if (view.name == "agendaWeek") {
                for(var i = 0; i < parameters.ClosedDays.length; i++) {
                    var index = (parameters.ClosedDays[i] > 0 ? parameters.ClosedDays[i] - 1 : 6);
                    $(".fc-agenda-days .fc-widget-content:eq(" + index + ")").addClass("fc-inactive");
                }
            }

            if (view.name == "agendaDay" && parameters.KoViewModel.FromCalendar()) {
                var cell = element.find("[class^='fc-slot'] .fc-widget-content");
                $(cell).css("vertical-align", "baseline");
                $(cell).removeClass("fc-inactive");
                $(cell).find("> div").text("");
                        
                view.allDayRow().find(".fc-day-content > div").text("");

                if (moment($("#calendar" + parameters.IdSala).fullCalendar("getDate").format("YYYY-MM-DD 00:00:00")) < moment(moment(new Date()).format("YYYY-MM-DD 00:00:00")))
                {
                    cell = element.find("[class^='fc-slot'] .fc-widget-content");
                    $(cell).css("vertical-align", "middle");
                    $(cell).addClass("fc-past-day");
                    view.allDayRow().find(".fc-day-content > div").text("Fecha pasada. Favor buscar durante otra fecha.");
                            
                    return false;
                }

                if (slotsDisponibles == 0) {
                    view.allDayRow().find(".fc-day-content > div").text("Sin espacios disponibles. Favor buscar durante otra fecha.");
                }
                else if (slotsDisponibles < dailySlots) {
                    view.allDayRow().find(".fc-day-content > div").text("Quedan " + slotsDisponibles + " espacios disponibles");
                }

                if (parameters.ClosedDays.indexOf($("#calendar" + parameters.IdSala).fullCalendar("getDate").day()) != -1) {
                    if (cell.length > 0) {
                        $(cell).css("vertical-align", "middle");
                        $(cell).addClass("fc-inactive");
                        $(cell).find("> div").text("No Disponible");
                    }
                }
            }
        },
        dayClick: function(date, allDay, jsEvent, view) {
            //if (allDay && $(this).is('td.fc-day')) {
            if (allDay && $(this).is('td.fc-day') && !$(this).is('td.fc-inactive') && !$(this).is('td.fc-past-day')) {
                $('#calendar' + parameters.IdSala).fullCalendar('changeView', 'agendaDay');
                parameters.KoViewModel.FromCalendar(true);
                $('#calendar' + parameters.IdSala).fullCalendar('gotoDate', date);
            }
        }
    });
};