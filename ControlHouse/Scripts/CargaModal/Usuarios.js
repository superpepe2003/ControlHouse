$("#btnNuevo").click(function (eve) {
    $("#modal-content").load("/Account/Register");
});

$(".btnCuentaEdit").click(function (eve) {
    $("#modal-content").load("/Account/Edit/" + $(this).data("id"));
});

$(".btnCuentaDetails").click(function (eve) {
    $("#modal-content").load("/Account/Details/" + $(this).data("id"));
});

$(".btnCuentaDelete").click(function (eve) {
    $("#modal-content").load("/Account/Delete/" + $(this).data("id"));
});