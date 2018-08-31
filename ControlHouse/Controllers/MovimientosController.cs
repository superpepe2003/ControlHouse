using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ControlHouse.Models;
using ControlHouse.Repository;
using ControlHouse.Helpers;
using ControlHouse.ViewModel;

namespace ControlHouse.Controllers
{    

    public class MovimientosController : Controller
    {
        private MovimientosRepository db = new MovimientosRepository();
        private CategoriasRepository catdb = new CategoriasRepository();
        private SubCategoriasRepository subcatdb = new SubCategoriasRepository();
        private CuentasRepository cuentaDB = new CuentasRepository();

        private MovimientosModel _Movimiento = new MovimientosModel();

        public static double _montoEdicion;
        public static bool? _tipo;
        public static int _cuentaAnterior;

        

        public List<DropDownLists> GeneraMenu()
        {
            List<DropDownLists> lista = new List<DropDownLists>();
            lista.Add(new DropDownLists("Editar", "Edit", "Movimientos"));
            lista.Add(new DropDownLists("Detalle", "Details", "Movimientos"));
            lista.Add(new DropDownLists("Eliminar", "Delete", "Movimientos"));
            return lista;
        }

        public JsonResult TieneMonto(double monto, int Id, int CuentaId, bool Tipo)
        {
            if (Tipo)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                var resultado = 0d;
                if (Id == 0)
                    resultado = cuentaDB.Devolver(CuentaId).Saldo;
                else
                    resultado = cuentaDB.Devolver(CuentaId).Saldo + _montoEdicion;

                if (resultado >= monto)
                    return Json(true, JsonRequestBehavior.AllowGet);
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }

        // GET: Movimientos        
        public ActionResult Index(int page=1, string sort="Fecha", string sortDir="DESC", FiltroModel oFiltro = null)
        {
            int NumeroDePersonas = db.ContarMovimientos(oFiltro);
            IEnumerable<MovimientosModel> mov;
            Direccion dir = sortDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ?
                                    Direccion.Ascendente :
                                    Direccion.Descendente;

            switch (sort.ToLower())
            {
                case "Fecha":
                    mov = db.Listar(oFiltro, page, 10, p => p.Fecha, dir);
                    break;
                case "Tipo":
                    mov = db.Listar(oFiltro, page, 10, p => p.Tipo, dir);
                    break;
                case "Categoria":
                    mov = db.Listar(oFiltro,page, 10, p => p.Categoria.Nombre, dir);
                    break;
                case "SubCategoria":
                    mov = db.Listar(oFiltro, page, 10, p => p.SubCategoria.Nombre, dir);
                    break;
                case "Cuenta":
                    mov = db.Listar(oFiltro, page, 10, p => p.Cuenta.Nombre, dir);
                    break;
                case "Descripcion":
                    mov = db.Listar(oFiltro, page, 10, p => p.Descripcion, dir);
                    break;
                case "Monto":
                    mov = db.Listar(oFiltro, page, 10, p => p.Monto, dir);
                    break;
                default:
                    mov = db.Listar(oFiltro, page, 10, p => p.Fecha, dir);
                    break;
            }
           
            MovimientosViewModel oViewModel = new MovimientosViewModel() { filtro = oFiltro, Movimientos = mov, NumeroDePersonas= NumeroDePersonas, PersonasPorPagina = 10  , valores = db.ListarEgresoIngreso(oFiltro)};
            ViewBag.Menu = GeneraMenu();
            return View(oViewModel);
        }

        // GET: Movimientos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientosModel movimientosModel = db.Devolver(id);
            if (movimientosModel == null)
            {
                return HttpNotFound();
            }
            return View(movimientosModel);
        }

        public JsonResult GetSubCategoria(int CategoriaId)
        {
            List<SubCategoriaModel> lista = subcatdb.Listar(CategoriaId);
            //var json = Json(lista, JsonRequestBehavior.AllowGet);
            //return json;
            var subCategorias = new List<SelectListItem>();
            foreach(var sc in lista)
            {
                subCategorias.Add(new SelectListItem { Text = sc.Nombre, Value = sc.Id.ToString() });
            }
            return Json(subCategorias, JsonRequestBehavior.AllowGet);

        }

        public ActionResult Crud(int id = 0)
        {
            var mov = (id > 0 ? db.Devolver(id)
                        : _Movimiento);

            if (mov.Id == 0)
                mov.Fecha = DateTime.Now;
            else
            {
                _montoEdicion = mov.Monto;
                _cuentaAnterior = mov.CuentaId;
                _tipo = mov.Tipo;
            }
            ViewBag.CategoriaId = new SelectList(catdb.Listar(), "Id", "Nombre", mov.CategoriaId);
            ViewBag.CuentaId = new SelectList(cuentaDB.Listar(), "Id", "Nombre", mov.CuentaId);
            ViewBag.SubCategoriaId = new SelectList(subcatdb.Listar(), "Id", "Nombre", mov.SubCategoriaId);
            return View(mov);
        }


        //VERIFICAR SI ES INGRESO O EGRESO EN LAS ALTAS DE MOVIMIENTOS DE LAS CUENTAS
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Guardar(MovimientosModel movimientoModel)
        {
            if (ModelState.IsValid)
            {
                if (movimientoModel.Id > 0)
                {


                    //si cambia el tipo
                    //if (_tipo != movimientoModel.Tipo)
                    //{
                        db.Actualizar(movimientoModel, _montoEdicion,_cuentaAnterior,_tipo.Value);
                    //}
                    //else
                    //{
                    //    _montoEdicion -= movimientoModel.Monto;
                    //    db.Actualizar(movimientoModel, _montoEdicion);
                    //}
                    _montoEdicion = 0;
                    _tipo = null;
                    _cuentaAnterior = 1;
                    return RedirectToAction("Index");
                }
                else
                    db.Grabar(movimientoModel);
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(catdb.Listar(), "Id", "Nombre", movimientoModel.CategoriaId);
            ViewBag.CuentaId = new SelectList(cuentaDB.Listar(), "Id", "Nombre", movimientoModel.CuentaId);
            ViewBag.SubCategoriaId = new SelectList(subcatdb.Listar(), "Id", "Nombre", movimientoModel.SubCategoriaId);
            return RedirectToAction("Crud", "Movimientos", movimientoModel);
        }

        // GET: Movimientos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MovimientosModel movimientosModel = db.Devolver(id);
            if (movimientosModel == null)
            {
                return HttpNotFound();
            }
            return View(movimientosModel);
        }

        // POST: Movimientos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Eliminar(db.Devolver(id));
            return RedirectToAction("Index");
        }

        public void CrearMovimientos()
        {
            MovimientosModel mov = new MovimientosModel { Fecha = DateTime.Now, CategoriaId = 8, SubCategoriaId = 1009, CuentaId = 7, Tipo = false, Monto = 0, Descripcion = null };
            Random rnd = new Random();
            for (var i=0;i<100;i++)
            {
                mov.Fecha = new DateTime(2017, 11, rnd.Next(1, 30), 0, 0, 0);
                mov.Monto = 25 * i;
                db.Grabar(mov);
            }
            
        }

        public JsonResult HayMonto()
        {           
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db = null;
                cuentaDB = null;
                subcatdb = null;
                catdb = null;
            }
            base.Dispose(disposing);
        }
    }
}
