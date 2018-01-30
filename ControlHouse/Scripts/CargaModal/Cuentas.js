$("#btnNuevo").click(function (eve) {
    $("#modal-content").load("/Cuenta/Create");
});

$(".btnCuentaEdit").click(function (eve) {
    $("#modal-content").load("/Cuenta/Edit/" + $(this).data("id"));
});

$(".btnCuentaDetails").click(function (eve) {
    $("#modal-content").load("/Cuenta/Details/" + $(this).data("id"));
});

$(".btnCuentaDelete").click(function (eve) {
    $("#modal-content").load("/Cuenta/Delete/" + $(this).data("id"));
});