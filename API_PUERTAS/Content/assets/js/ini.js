$(document).ready(function () {

    //$("#archivo").change(function () {
    //    $("#enviar-registro").prop("disabled", this.files.length == 0);
    //});

    $(function () {
        $("#mdb-lightbox-ui").load("mdb-addons/mdb-lightbox-ui.html");
    });

    $("body").on('click', 'button', function () {

        // Si el boton no tiene el atributo ajax no hacemos nada
        if ($(this).data('ajax') == undefined) return;

        // El metodo .data identifica la entrada y la castea al valor más correcto
        if ($(this).data('ajax') != true) return;


        //var vidFileLength = $("#archivo")[0].files.length;

        ///*Aquí validas si no han seleccionado archivos mandas una alerta y se detiene el 
        // script*/
        //if (vidFileLength === 0) {

        //    Swal.fire({
        //        title: "Cuidado !!", text: "Debe de ingresar un archivo, este archivo depende del tipo de modalidad con el cual efectúa su registro.", icon: "error"
        //    }).then((result) => { return; });


        //}

            var form = $(this).closest("form");
            var buttons = $("button", form);
            var button = $(this);
            var url = form.attr('action');

            if (button.data('confirm') != undefined) {
                if (button.data('confirm') == '') {
                    if (!confirm('¿Esta seguro de realizar esta acción?')) return false;
                } else {
                    if (!confirm(button.data('confirm'))) return false;
                }
            }

            if (button.data('delete') != undefined) {
                if (button.data('delete') == true) {
                    url = button.data('url');
                }
            } else {
                if (!form.valid()) {
                    return false;
                }
            }


            // Creamos un div que bloqueara todo el formulario
            var block = $('<div class="block-loading" />');
            form.prepend(block);

            // En caso de que haya habido un mensaje de alerta
            $(".alert", form).remove();

            // Para los formularios que tengan CKupdate
            if (form.hasClass('CKupdate')) CKupdate();

            form.ajaxSubmit({
                dataType: 'JSON',
                type: 'POST',
                url: url,
                success: function (r) {
                    block.remove();
                    if (r.response) {
                        console.log(r.response);
                        if (!button.data('reset') != undefined) {
                            if (button.data('reset')) form[0].reset();

                        }
                        else {
                            form.find('input:file').val('');
                        }
                    }

                    // Mostrar mensaje
                    if (r.message != null) {
                        if (r.message.length > 0) {
                            var css = "";
                            var titulo = "";
                            var icono = "";
                            if (r.response) {

                                css = "success";
                                titulo = "¡ Buen Trabajo !";
                                icono = "success";
                            }
                            else {

                                css = "danger";
                                titulo = "Error";
                                icono = "error";
                            }
                            //swal({ title: titulo, text: r.message, type: css });
                            Swal.fire({
                                title: titulo, text: r.message, icon: icono
                            }).then((result) => { location.reload(); });
                            //var message = '<div class="alert ' + css + ' alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + r.message + '</div>';
                            //form.prepend(message);


                        }
                    }

                    // Ejecutar funciones
                    if (r.function != null) {
                        setTimeout(r.function, 0);
                    }
                    // Redireccionar
                    if (r.href != null) {
                        if (r.href == 'self') {

                            window.location.reload(true);
                        }
                        else {

                            window.location.href = r.href;
                        }
                    }


                },
                error: function (jqXHR, textStatus, errorThrown) {
                    block.remove();
                    form.prepend('<div class="alert alert-warning alert-dismissable"><button type="button" class="close" data-dismiss="alert" aria-hidden="true">&times;</button>' + errorThrown + ' | <b>' + textStatus + '</b></div>');
                }
            });

            return false;
        });


})

jQuery.fn.reset = function () {
    $("input:password,input:file,input:file,input:text,textarea", $(this)).val('');
    $("input:checkbox:checked", $(this)).click();
    $("select").each(function () {
        $(this).val($("option:first", $(this)).val());
    })
};