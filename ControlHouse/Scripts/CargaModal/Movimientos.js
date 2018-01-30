$(document).ready(function () {

    $("#btnNuevo").click(function (eve) {
        $("#modal-content").load("/Movimientos/Crud/");
    });

    $(document).on("click",".btnMovimientosEdit", function (eve) {
        $("#modal-content").val('');
        var $id = $(this).data('id');
        $("#modal-content").load("/Movimientos/Crud/" + $id);
    });

    $(".btnMovimientosDetails").click(function (eve) {
        $("#modal-content").load("/Movimientos/Details/" + $(this).data("id"));
    });

    $(".btnMovimientosDelete").click(function (eve) {
        $("#modal-content").load("/Movimientos/Delete/" + $(this).data("id"));
    });

    var data = $('[data-marker]').data('marker');

    if(data.FechaInicial != null) {
        $("input[type='date']").attr('disabled', false);
        $("#chkFechas").prop('checked', true);
    }
    else {
        $("input[type='date']").attr('disabled', 'disabled');
        $("#chkFechas").prop('checked', false);
    }

    if(data.Tipo != null) {
        $("select").attr('disabled', false);
        $("#chkTipo").prop('checked', true);
        if (data.Tipo) {
            $("#option1").attr('selected', 'selected');
        }
        else {
            //$("#option1").removeAttr('selected');
            $("#option2").attr('selected', 'selected');
        }
    }
    else {
        $("select").attr('disabled', 'disabled');
        $("#chkTipo").prop('checked', false);
    }


    $("#chkFechas").on('click', function () {
        if ($(this).is(":checked")) {
            $("input[type='date']").attr('disabled', false);
        }
        else {
            $("input[type='date']").attr('disabled', 'disabled');
        }
    });

    $("#chkTipo").on('click', function () {
        if ($(this).is(":checked")) {
            $("select").attr('disabled', false);
        }
        else {
            $("select").attr('disabled', 'disabled');
        }
    });

});

//document.getElementById('#myModal').on('show.bs.modal', function (event) {
//    var modal = $(this);
//    modal.find('#modal-content').val("/Movimientos/Create");
//});
