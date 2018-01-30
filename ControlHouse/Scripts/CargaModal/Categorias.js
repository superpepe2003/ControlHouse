$("#btnNuevo").click(function (eve) {
    $("#modal-content").load("/Categoria/Create");
});

$(".btnCategoriaEdit").click(function (eve) {
    $("#modal-content").load("/Categoria/Edit/" + $(this).data("id"));
});

$(".btnCategoriaDetails").click(function (eve) {
    $("#modal-content").load("/Categoria/Details/" + $(this).data("id"));
});

$(".btnCategoriaDelete").click(function (eve) {
    $("#modal-content").load("/Categoria/Delete/" + $(this).data("id"));
});
