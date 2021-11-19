var perf = {};
(function ($, window, document) {
    $(function () {
        $(document).on("click", ".perf-detail-link", function () {
             //llamado de ajax
             if ($.active == 0) { //Validamos que no exista otra petición activa
                 $.ajax({
                     dataType: 'json',
                     type: "GET",
                     cache: false,
                     url: document.location.origin + "/Perfil/RenderFormDetail",
                     data: {
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
 
                         $('#divRenderPerfilDetails').html(data.html);
                         $("#modalPerfilDetails").modal('show');
                     },
                     error: function (jqXHR, exception) {
                         app.ajaxError(jqXHR, exception);
                     },
                     complete: function () {
                         //$('.ChangeState').change();
                     }
                 });
             }
         });
        $(document).on("click", ".perf-delete-link", function () {
            var key = $(this).data("id");
            //llamado de ajax
            if ($.active == 0) { //Validamos que no exista otra petición activa
                bootbox.dialog({
                    title: 'Eliminar',
                    message: "¿Esta seguro de eliminar Perfil?",
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
                                    url: document.location.origin + "/Perfil/DeletePerfil",
                                    data: JSON.stringify({ "key": key }),
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
                                        window.location.href = document.location.origin + "/Perfil";
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