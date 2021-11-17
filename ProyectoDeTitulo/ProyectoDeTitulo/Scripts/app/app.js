var app = {};
var dataTableEspanol = {
    "sProcessing": "Procesando...",
    "sLengthMenu": "Mostrar _MENU_ registros",
    "sZeroRecords": "No se encontraron resultados",
    "sEmptyTable": "Ningún dato disponible en esta tabla",
    "sInfo": "Mostrando registros del _START_ al _END_ de un total de _TOTAL_ registros",
    "sInfoEmpty": "Mostrando registros del 0 al 0 de un total de 0 registros",
    "sInfoFiltered": "(filtrado de un total de _MAX_ registros)",
    "sInfoPostFix": "",
    "sInfoThousands": ",",
    "sSearch": "Buscar: ",
    "sUrl": "",
    "sLoadingRecords": "Cargando...",
    "oPaginate": {
        "sFirst": "Primero",
        "sLast": "Último",
        "sNext": "Siguiente",
        "sPrevious": "Anterior",
    },
    "oAria": {
        "sSortAscending": ": Activar para ordenar la columna de manera ascendente",
        "sSortDescending": ": Activar para ordenar la columna de manera descendente"
    }
};

(function () {
    //Mensaje de error del controlador.
    app.error = function (text) {
        app.NotificationError(text);
    };
    //Mensaje de error del request.
    app.ajaxError = function (jqXHR, exception) {
        $(".modal-backdrop.in").hide();
        let mensaje = "Error";

        if (jqXHR.status === 0) {
            mensaje += ": " + exception;
        } else if (jqXHR.status == 404) {
            mensaje += ': Ruta no encontrada [404].';
        } else if (jqXHR.status == 500) {
            mensaje += ': Error interno del servidor [500].';
        } else if (jqXHR.status == 500) {
            mensaje += ': No autorizado [401].';
        } else if (exception === 'parsererror') {
            mensaje += ': Requested JSON parse failed.';
        } else if (exception === 'timeout') {
            mensaje += ': Time out error.';
        } else if (exception === 'abort') {
            mensaje += ': Ajax request abortada.';
        } else {
            mensaje += ' Desconocido: ' + jqXHR.responseText;
        }

        app.NotificationError(mensaje);
        //console.log(jqXHR.responseText);
    }

    app.NotificationDocumentSuccess = function (result) {
        $.notifyClose();
        let mensaje1 = '<tr><td style="text-align: left">Total</td><td style="text-align: right;font-weight: bold">' + app.FormatDecimalText(result.PagoTotal, decimalImporte) + '</td></tr>';
        let mensaje = '<table style="width: 100%">' + mensaje1 + '</table>';

        $.notify({
            // options
            icon: 'glyphicon glyphicon-ok',
            title: "Documento registrado con éxito <strong>N° " + result.DocumentoDocNum + "</strong>",
            message: mensaje,
        }, {
                // settings
                element: 'body',
                position: null,
                type: "success",
                allow_dismiss: true,
                newest_on_top: true,
                showProgressbar: false,
                placement: {
                    from: "bottom",
                    align: "right"
                },
                offset: 20,
                spacing: 5,
                z_index: 1031,
                delay: 10000,
                timer: 1000,
                url_target: '_blank',
                mouse_over: null,
                animate: {
                    enter: 'animated bounceInRight',
                    exit: 'animated lightSpeedOut'
                },
                onShow: null,
                onShown: null,
                onClose: null,
                onClosed: null,
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">&nbsp;&nbsp;{1}</span><br/>' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'

            });
        console.log("4.- Notificacion Success");
    };
    app.NotificationPaymentSuccess = function (result) {
        $.notifyClose();
        let titulo = result.PantallaInicio == "BOLE" ? "Boleta" :
            result.PantallaInicio == "FACT" ? "Factura" :
                result.PantallaInicio == "COTI" ? "Cotización" :
                    result.PantallaInicio == "NOTA" ? "Nota crédito" :
                        result.PantallaInicio == "ORDE" ? "Orden de venta" :
                            result.PantallaInicio == "GIFT" ? "Gift Card" : "Documento";

        let mensaje1 = '<tr><td style="text-align: left">Total</td><td style="text-align: right;font-weight: bold">' + app.FormatDecimalText(result.PagoTotal, decimalImporte) + '</td></tr>';
        let mensaje2 = '';
        let mensaje3 = '';
        if (result.PagoMonto_multiple) {
            mensaje2 = result.PagoMonto_str;
        } else {
            mensaje2 = (result.PagoMontoEfectivo > 0 ? '<tr><td style="text-align: left">Efectivo</td><td style="text-align: right;font-weight: bold">' + app.FormatDecimalText(result.PagoMontoEfectivo, decimalImporte) + '</td></tr>' : '');
            mensaje3 = (result.PagoMontoEfectivo > 0 ? '<tr><td style="text-align: left">Vuelto</td><td style="text-align: right;font-weight: bold">' + app.FormatDecimalText(result.PagoMontoVuelto, decimalImporte) + '</td></tr>' : '');
        }




        let mensaje = '<table style="width: 100%">' + mensaje1 + mensaje2 + mensaje3 + '</table>';

        if (result.DocumentoDocNum != null) {
            messageResult = "Pago correcto <strong>" + titulo + " N° " + result.DocumentoDocNum + "</strong>";
        } else {
            messageResult = "Pago correcto <strong>" + titulo + "</strong>";
        }

        $.notify({
            // options

            icon: 'glyphicon glyphicon-ok',
            title: messageResult,
            message: mensaje,
        }, {
                // settings
                element: 'body',
                position: null,
                type: "success",
                allow_dismiss: true,
                newest_on_top: true,
                showProgressbar: false,
                placement: {
                    from: "bottom",
                    align: "right"
                },
                offset: 20,
                spacing: 5,
                z_index: 999999999999,
                delay: 15000,
                timer: 1000,
                url_target: '_blank',
                mouse_over: null,
                animate: {
                    enter: 'animated bounceInRight',
                    exit: 'animated lightSpeedOut'
                },
                onShow: null,
                onShown: null,
                onClose: null,
                onClosed: null,
                icon_type: 'class',
                template: '<div data-notify="container" class="col-xs-11 col-sm-3 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">&nbsp;&nbsp;{1}</span><br/>' +
                    '<span data-notify="message">{2}</span>' +
                    '<div class="progress" data-notify="progressbar">' +
                    '<div class="progress-bar progress-bar-{0}" role="progressbar" aria-valuenow="0" aria-valuemin="0" aria-valuemax="100" style="width: 0%;"></div>' +
                    '</div>' +
                    '<a href="{3}" target="{4}" data-notify="url"></a>' +
                    '</div>'
            });
    };
    app.NotificationSuccess = function (message) {
        $.notifyClose();
        app.Notification(message, "SUCCESS");
    };
    app.NotificationError = function (message) {
        app.Notification(message, "ERROR");
    };
    app.NotificationInfo = function (message) {
        app.Notification(message, "INFO");
    };
    app.NotificationWarning = function (message) {
        app.Notification(message, "WARNING");
    };
    app.Notification = function (message, tipo) {
        let _icono = "icon fa fa-ban";
        let _title = "Ha ocurrido un error";
        let _type = "error";

        if (tipo == "INFO") {
            _icono = "icon fa fa-info";
            _title = "Información";
            _type = "info";
        }
        else if (tipo == "SUCCESS") {
            _icono = "icon fa fa-check";
            _title = "Correcto";
            _type = "success";
        }
        else if (tipo == "WARNING") {
            _icono = "icon fa fa-warning";
            _title = "Advertencia";
            _type = "warning";
        }

        $.notify({
            icon: _icono,
            title: _title,
            message: message,
        }, {
                type: _type,
                delay: 10000,
                timer: 500,
                spacing: 1,
                z_index: 9999,
                newest_on_top: true,
                offset: {
                    x: 0,
                    y: 0
                },
                placement: {
                    from: "top",
                    align: "center"
                },
                animate: {
                    enter: 'animated bounceInDown',
                    exit: 'animated fadeOutRight'
                },
                template: '<div data-notify="container" class="col-xs-11 col-sm-11 alert alert-{0}" role="alert">' +
                    '<button type="button" aria-hidden="true" class="close" data-notify="dismiss">×</button>' +
                    '<span data-notify="icon"></span> ' +
                    '<span data-notify="title">{1}</span><br/>' +
                    '<span data-notify="message">{2}</span>' +
                    '</div>'
            });
    };

    app.SessionClose = function (mensaje) {
        bootbox.alert({
            message: mensaje,
            buttons: {
                ok: {
                    label: '<i class="fa fa-check"></i> Aceptar',
                    className: 'btn-lg btn-primary'
                }
            },
            callback: function () {
                $("#btnLogoutSession").click();
            }
        });
    };
})();

