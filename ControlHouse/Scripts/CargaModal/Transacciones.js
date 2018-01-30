$("#btnNuevo").click(function (eve) {
    $("#modal-content").load("/Transaccion/Crud/");
});

$(".btnTransaccionEdit").click(function (eve) {
    $("#modal-content").load("/Transaccion/Crud/" + $(this).data("id"));
});

$(".btnTransaccionDetails").click(function (eve) {
    $("#modal-content").load("/Transaccion/Details/" + $(this).data("id"));
});

$(".btnTransaccionDelete").click(function (eve) {
    $("#modal-content").load("/Transaccion/Delete/" + $(this).data("id"));
});


var data = $('[data-marker]').data('marker');

if (data.FechaInicial != null) {
    $("input[type='date']").attr('disabled', false);
    $("#chkFechas").prop('checked', true);
}
else {
    $("input[type='date']").attr('disabled', 'disabled');
    $("#chkFechas").prop('checked', false);
}



$("#chkFechas").on('click', function () {
    if ($(this).is(":checked")) {
        $("input[type='date']").attr('disabled', false);
    }
    else {
        $("input[type='date']").attr('disabled', 'disabled');
    }
});



//$(document).ready(function () {
//    $("#frm-transacciones").submit(function () {
//        var form = $(this);
//        if (form.validate()) {

//        }
//    })
//})

//$(document).ready(function() {
//    $("#frm-transacciones").submit(function () {
//        var form = $(this);
//        if(form.validate())
//        {
//            //form.ajaxSubmit({
//            //    dataType: 'JSON',
//            //    type: 'POST',
//            //    url: form.attr('action'),
//            //    success: function (r) {
//            //        if (r.respuesta) {
//            //            window.location.href = r.redirect;
//            //        }
//            //        else {
//            //            $("#cursos-validacion").text(r.error)
//            //                                    .show();
//            //        }
//            //    },
//            //    error: function (jqXHR, textStatus, errorThrown) {
//            //        alert(errorThrown);
//            //    }
//            //});
//        }
//    });
//});