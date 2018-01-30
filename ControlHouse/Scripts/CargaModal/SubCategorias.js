$("#btnNuevo").click(function (eve) {
    $("#modal-content").load("/SubCategoria/Create");
});

$(".btnSubCategoriaEdit").click(function (eve) {
    $("#modal-content").load("/SubCategoria/Edit/" + $(this).attr("data-id"));
});

$(".btnSubCategoriaDetails").click(function (eve) {
    $("#modal-content").load("/SubCategoria/Details/" + $(this).data("id"));
});

$(".btnSubCategoriaDelete").click(function (eve) {
    $("#modal-content").load("/SubCategoria/Delete/" + $(this).data("id"));
});