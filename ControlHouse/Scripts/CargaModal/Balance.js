var cantidad = $("[data-cantidad]").data("cantidad");
var meses = $("[data-meses]").data("meses");
calculo();

$("input[type='text']").focusout(function () {
    var a = $(this).val();
    if (a === "")
        a = "0";
    $(this).attr("placeholder", a);
    calculo();
});

function calculo() {
    calculaIngresos(true);
    calculaIngresos(false);
    calculaTotales();
}

function calculaIngresos(tipo) {
    var montoTotal = 0;
    var monto = 0;
    for (var s in meses) {
        monto = 0;
        var tip = (tipo) ? cantidad.Ingresos : cantidad.Egresos;
        var tip = tip + 3;
        var nomTip = (tipo) ? "#Ingreso" : "#Egreso";
        var nomTipResumen = (tipo) ? "#Ingresoresumen" : "#Egresoresumen";
        for (var i = 0; i < tip; i++) {
            var id = nomTip + meses[s] + i;
            var a = $(id).attr("placeholder");
            if (a == "" || a <= 0)
                var a = "0";
            monto = parseFloat(monto) + parseFloat(a);
        }
        var idLabel = nomTip + meses[s];
        var idlabelresumen = nomTipResumen + meses[s];
        $(idLabel).text(monto);
        $(idlabelresumen).text(monto);
    }
}

function calculaTotales() {
    var total_acum = parseFloat($('#TotalAcumulado').text());
    for (var s in meses) {
        var idlabelIngreso = "#Ingreso" + meses[s];
        var idlabelEgreso = "#Egreso" + meses[s];
        var idTotal = "#Total" + meses[s];
        var idTotalAcum = "#TotalAcumulado" + meses[s];
        var a = $(idlabelIngreso).text();
        var b = $(idlabelEgreso).text();
        total_acum = parseFloat(total_acum) + parseFloat(a) - parseFloat(b);
        var suma = parseFloat(a) - parseFloat(b);
        if (suma < 0)
            $(idTotal).css("background-color", "red");
        $(idTotal).text(suma);
        $(idTotalAcum).text(total_acum);
    }
}

$(document).ready(function () {

    $('#tbBalance').DataTable({
        dom: 'Bfrtip',
        paging: false,
        searching: false,
        columnDefs: [{ orderable: false, targets: [12] }],
        ordering: false,
        buttons: [
            {
                extend: 'pdf',
                orientation: 'landscape',
                text: 'Grabe a PDF',
                exportOptions: {
                    modifier: {
                        page: 'current'
                    }
                }
            }
        ]
    });
});