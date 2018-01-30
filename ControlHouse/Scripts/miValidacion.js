$(document).ready(function () {


    // 2. REGISTRAR REGLA DE VALIDACIÓN, MÉTODO DE VALIDACIÓN Y MENSAJE POR DEFECTO

    $.validator.addMethod("different", function (value, element, param) {
        //value = value.toLowerCase();
        var value2 = ($(param).val());
        if (value != value2) {               
            return true; //supera la validación
        }
        else {                
            return false; //error de validación               
        }
    }
    , "LAS CUENTAS NO PUEDEN SER IGUALES");

    //function EsMayor(value, element, param) {
    //    var idCuenta = { idCuenta: ($(param).val()) };
    //     $.ajax({
    //        method: "POST",
    //        async:false,
    //        url: "/Transaccion/GetMonto",
    //        data: JSON.stringify(idCuenta),
    //        contentType: "application/json; charset-utf-8",
    //        dataType: "json",
    //        success: function (monto) {
    //            if (value > monto) {
    //                return false;
    //            }
    //            else {
    //                return true;
    //            }
    //        }
    //    });
    //}

    //$.validator.addMethod("MontoSuficiente", EsMayor, "LA CUENTA ORIGEN NO TIENE SALDO SUFICIENTE");
              
    $("#CuentaIdDestino").rules("add", {
        different: ("#CuentaIdOrigen")
    });

    //$("#Monto").rules("add", {
    //    MontoSuficiente: ("#CuentaIdOrigen"),
    //    messages: {
    //        MontoSuficiente: "NO HAY CASH"
    //    }
    //});
        
});


//$(function () {
//    $.validator.addMethod("MontoSuficiente", function (value, element, param) {
//        var value2 = ($(param).val());
//        var data = {
//            id: value2,
//            monto: value
//        }

//        $.ajax({
//            method: "POST",
//            url: "/Transaccion/GetMonto",
//            data: JSON.stringify(data),
//            contentType: "application/json; charset-utf-8",
//            dataType: "json"
//        }).done(function (info) {
//            return false;
//        })
//    }, "LA CUENTA ORIGEN NO TIENE SALDO SUFICIENTE");











//(function ($) {
//    $.validator.unobtrusive.adapters.addSingleVal("comparenoesigual", "param");
//    $.validator.addMethod('comparenoesigual', function (value, element, param) {
//        if ($("#" + param).value != value) {
//            return true;
//        }
//        return false;
//    });
////})(jQuery);

//function distint(value, element, param) {
//    //value es el valor actual del elemento que se está validando
//    //element es el elemento DOM que se está validando
//    //param son los parámetros especificados por el método
//    //  p. ej. en el caso de minlength="3", param será 3
//    //  en caso de que el método no tenga parámetros, param será true

//    //if (value != param) {
//    //    return true; //supera la validación
//    //}
//    //else {
//    //    return false; //error de validación
//    //}
//    return false;
//}

//// 2. REGISTRAR REGLA DE VALIDACIÓN, MÉTODO DE VALIDACIÓN Y MENSAJE POR DEFECTO
//$(function(){
//    $.validator.addMethod("distint", function (value, element) {
//        return false;
//    }, "Las cuentas no pueden ser iguales CLIENTE");
//})

//// 3. UTILIZAR NUESTRA NUEVA REGLA DE VALIDACIÓN

//$().ready(function () {
//    $("#frm").validate({
//        debug:true,
//        rules: {
//            CuentaIdDestino: {
//                distint: $("CuentaIdOrigen").val()
//            }
//        }        
//    });
//});

//$("#CuentaIdDestino").rules("add", {
//    distint: true,
//    messages: {
//        distint: "NO ES CUENTA CLIENTE"
//    }
//});




