var usr = {};
(function ($, window, document) {
    $(function () {

       /* $(document).on("click", "#btnUsuarioCrearNuevo", function () {
            //llamado de ajax
            if ($.active == 0) { //Validamos que no exista otra petición activa
                $.ajax({
                    dataType: 'json',
                    type: "GET",
                    cache: false,
                    url: document.location.origin + "/User/RenderFormClient",
                    data: {
                        'create': true,
                        'key': ""
                    },
                    success: function (data) {

                        if (data.success == false) {
                            if (data.SessionClose != undefined && data.SessionClose != null && data.SessionClose) {
                                app.SessionClose(data.responseText);
                            }
                            else {
                                app.error(data.responseText);
                            }
                            return;
                        }

                        $('#divRenderCreateUsuario').html(data.html);
                        $("#modalCreateUsuario").modal('show');
                    },
                    error: function (jqXHR, exception) {
                        app.ajaxError(jqXHR, exception);
                    },
                    complete: function () {
                        $('.ChangeState').change();
                    }
                });
            }
        });
        $(document).on("click", ".usr-edit-link", function () {
            //llamado de ajax
            if ($.active == 0) { //Validamos que no exista otra petición activa
                $.ajax({
                    dataType: 'json',
                    type: "GET",
                    cache: false,
                    url: document.location.origin + "/User/RenderFormClient",
                    data: {
                        'create': false,
                        'key': $(this).data("id")
                    },
                    success: function (data) {

                        if (data.success == false) {
                            if (data.SessionClose != undefined && data.SessionClose != null && data.SessionClose) {
                                app.SessionClose(data.responseText);
                            }
                            else {
                                app.error(data.responseText);
                            }
                            return;
                        }

                        $('#divRenderEditUsuario').html(data.html);
                        $("#modalEditUsuario").modal('show');
                    },
                    error: function (jqXHR, exception) {
                        app.ajaxError(jqXHR, exception);
                    },
                    complete: function () {
                        $('.ChangeState').change();
                    }
                });
            }
        });*/
        $(document).on("click", ".usr-delete-link", function () {
            var key = $(this).data("id");
            //llamado de ajax
            if ($.active == 0) { //Validamos que no exista otra petición activa
                bootbox.dialog({
                    title: 'Eliminar',
                    message: "¿Esta seguro de eliminar Usuario?",
                    buttons: {
                        cancel: {
                            label: '<i class="fa fa-times"></i> Cancelar',
                            className: 'btn-lg btn-default'
                        },
                        close: {
                            label: '<i class="fa fa-check"></i> Eliminar',
                            className: 'btn btn-lg btn-primary',
                            callback: function () {
                                $.ajax({
                                    contentType: 'application/json; charset=utf-8',
                                    dataType: 'json',
                                    type: "POST",
                                    cache: false,                                    
                                    url: document.location.origin + "/User/DeleteUser",
                                    data: JSON.stringify({"key": key}),
                                    success: function (data) {

                                        if (data.success == false) {
                                            if (data.SessionClose != undefined && data.SessionClose != null && data.SessionClose) {
                                                app.SessionClose(data.responseText);
                                            }
                                            else {
                                                app.error(data.responseText);
                                            }
                                            return;
                                        }                                        
                                    },
                                    error: function (jqXHR, exception) {
                                        app.ajaxError(jqXHR, exception);
                                    },
                                    complete: function () {
                                        $('.ChangeState').change();
                                    }
                                });
                            }
                        }
                    }
                });
            }
        });

    });
}(window.jQuery, window, document));