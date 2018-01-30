using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace ControlHouse.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new StyleBundle("~/bundles/css")
                .Include(                
                "~/Content/site.css",
                "~/Content/bootstrap.min.css",
                "~/Content/myBootstrap.css"
                //"~/Content/vendor/datatables/dataTables.bootstrap4.css",
                //"~/Content/css/sb-admin.css"
                //"~/Content/vendor/font-awesome/css/font-awesome.min.css",
                ));


            bundles.Add(new ScriptBundle("~/bundles/js")
               .Include(
                "~/Scripts/modernizr-2.6.2.js",
                "~/Scripts/bootstrap.min.js",
                "~/Scripts/jquery-1.10.2.min.js",
                "~/Scripts/jquery-1.10.2.js",
                "~/Scripts/jquery.validate.js",
                "~/Scripts/jquery.validate.min.js",
                "~/Scripts/jquery.validate.unobtrusive.js",
                "~/Scripts/jquery.validate.unobtrusive.min.js",
                "~/Scripts/jquery.unobtrusive-ajax.js",
                "~/Scripts/jquery.unobtrusive-ajax.min.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Movimientosjs")
                .Include(
                "~/Scripts/CargaModal/Movimientos.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Cuentasjs")
                .Include(
                "~/Scripts/CargaModal/Cuentas.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Categoriasjs")
                .Include(
                "~/Scripts/CargaModal/Categorias.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/SubCategoriasjs")
                .Include(
                "~/Scripts/CargaModal/SubCategorias.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Transaccionjs")
                .Include(
                "~/Scripts/CargaModal/Transacciones.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/Balancejs")
                .Include(
                "~/Scripts/CargaModal/Balance.js"
                ));

            bundles.Add(new ScriptBundle("~/bundles/miVal").Include(
                       "~/Scripts/miValidacion.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/jqdatatable").Include(
                "~/Scripts/DataTables/buttons.html5.js",
                "~/Scripts/DataTables/buttons.html5.min.js",
                "~/Scripts/DataTables/buttons.print.js",
                "~/Scripts/DataTables/buttons.print.min.js",
                "~/Scripts/DataTables/jquery.dataTables.js",
                "~/Scripts/DataTables/jquery.dataTables.min.js"
                ));
        }
    }
}