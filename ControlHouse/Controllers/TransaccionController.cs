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
    public class TransaccionController : Controller
    {
        private TransaccionesRepository _repository = new TransaccionesRepository();
        private CuentasRepository _repoCuentas = new CuentasRepository();

        public TransaccionModel transaccion = new TransaccionModel();
        public static double _montoViejo;

        public List<DropDownLists> GeneraMenu()
        {
            List<DropDownLists> lista = new List<DropDownLists>();
            lista.Add(new DropDownLists("Editar", "Edit", "Transaccion"));
            lista.Add(new DropDownLists("Detalle", "Details", "Transaccion"));
            lista.Add(new DropDownLists("Eliminar", "Delete", "Transaccion"));
            return lista;
        }

        // GET: Transaccion
        public ActionResult Index(int page=0, string sort="Fecha", string sortDir="DESC", FiltroModel oFiltro=null)
        {
            int NumeroDePersonas = _repository.ContarTransacciones(oFiltro);
            IEnumerable<TransaccionModel> trans;
            Direccion dir = sortDir.Equals("ASC", StringComparison.CurrentCultureIgnoreCase) ?
                                    Direccion.Ascendente :
                                    Direccion.Descendente;

            switch (sort.ToLower())
            {
                case "Fecha":
                    trans = _repository.Listar(oFiltro, page, 10, p => p.Fecha, dir);
                    break;
                case "CuentaOrigen":
                    trans = _repository.Listar(oFiltro, page, 10, p => p.CuentaOrigen.Nombre, dir);
                    break;
                case "CuentaDestino":
                    trans = _repository.Listar(oFiltro, page, 10, p => p.CuentaDestino.Nombre, dir);
                    break;
                case "Descripcion":
                    trans = _repository.Listar(oFiltro, page, 10, p => p.Descripcion, dir);
                    break;
                case "Monto":
                    trans = _repository.Listar(oFiltro, page, 10, p => p.Monto, dir);
                    break;
                default:
                    trans = _repository.Listar(oFiltro, page, 10, p => p.Fecha, dir);
                    break;
            }

            TransaccionesViewModel oViewModel = new TransaccionesViewModel() { filtro = oFiltro, Transaccion = trans, NumeroDePersonas = NumeroDePersonas, PersonasPorPagina = 10 };
            ViewBag.Menu = GeneraMenu();
            return View(oViewModel);
        }

        // GET: Transaccion/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransaccionModel transaccionModel = _repository.Devolver(id);
            if (transaccionModel == null)
            {
                return HttpNotFound();
            }
            return View(transaccionModel);
        }

        // GET: Transaccion/Create
        //public ActionResult Create()
        //{
        //    ViewBag.CuentaIdOrigen = new SelectList(_repoCuentas.Listar(), "Id", "Nombre");
        //    ViewBag.CuentaIdDestino = new SelectList(_repoCuentas.Listar(), "Id", "Nombre");
        //    ViewBag.Fecha = DateTime.Now;
        //    return View();
        //}

        public ActionResult Crud(int id = 0)
        {
            var trans = (id > 0 ? _repository.Devolver(id)
                        : transaccion);
            _montoViejo = trans.Monto;

            ViewBag.CuentaIdOrigen = new SelectList(_repoCuentas.Listar(), "Id", "Nombre", trans.CuentaIdOrigen);
            ViewBag.CuentaIdDestino = new SelectList(_repoCuentas.Listar(), "Id", "Nombre", trans.CuentaIdDestino);
            if (id == 0)
                trans.Fecha = DateTime.Now;          
            return View(trans);
        }


       // POST: Transaccion/Create
       // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse.Para obtener
       // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.

       //VALIDACION LADO DEL SERVIDOR

       [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Fecha,CuentaIdOrigen,CuentaIdDestino,Monto,Descripcion")] TransaccionModel transaccionModel)
        {
            if (ModelState.IsValid)
            {
                if (transaccionModel.Id > 0)
                {
                    if (_montoViejo != transaccionModel.Monto)
                        _montoViejo = _montoViejo - transaccionModel.Monto;
                    _repository.Grabar(transaccionModel, _montoViejo);
                    return RedirectToAction("Index");
                }
                else
                    _repository.Grabar(transaccionModel, 0);
                _montoViejo = 0;
                return RedirectToAction("Index");
            }
            ViewBag.CuentaIdOrigen = new SelectList(_repoCuentas.Listar(), "Id", "Nombre");
            ViewBag.CuentaIdDestino = new SelectList(_repoCuentas.Listar(), "Id", "Nombre");
            return RedirectToAction("Crud","Transaccion",transaccionModel);
        }       

        // GET: Transaccion/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TransaccionModel transaccionModel = _repository.Devolver(id);
            CuentaModel cuentaDestino = _repoCuentas.Devolver(transaccionModel.CuentaIdDestino);
            //Verifico si la cuenta destino tiene el monto para devolverlo
            if (cuentaDestino.Saldo < transaccionModel.Monto)
                ViewBag.CuentaDestino = false;
            else
                ViewBag.CuentaDestino = true;

            if (transaccionModel == null)
            {
                return HttpNotFound();
            }
            return View(transaccionModel);
        }

        // POST: Transaccion/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            TransaccionModel transaccionModel = _repository.Devolver(id);
            CuentaModel cuentaDestino = _repoCuentas.Devolver(transaccionModel.CuentaIdDestino);
            //Verifico si la cuenta destino tiene el monto para devolverlo
            if (cuentaDestino.Saldo >= transaccionModel.Monto)
                _repository.Eliminar(transaccionModel);            
            
            return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetMonto(int idCuenta)
        {
            double c = 0;
            //if (idCuenta.HasValue)
            //{
                CuentaModel cuenta = _repoCuentas.Devolver(idCuenta);
            //    c = cuenta.Saldo;
            //}            
            var cjson = Json(cuenta.Saldo);

            return cjson;
        }


        //Retorna true si la cuentaIdOrigen tiene el monto para hacer la operacion y en caso que este editando suma el valor anterior de la transaccion para saber si lo puede hacer o no
        public JsonResult HayMonto(double monto, int Id, int CuentaIdOrigen)
        {
            var resultado = 0d;
            if (Id == 0)
                resultado = _repoCuentas.Devolver(CuentaIdOrigen).Saldo;
            else
                resultado = _repoCuentas.Devolver(CuentaIdOrigen).Saldo + _montoViejo;

            if (resultado >= monto)
                return Json(true, JsonRequestBehavior.AllowGet);
            return Json(false, JsonRequestBehavior.AllowGet);
        }

        //public JsonResult Retornar(string Apellido)
        //{
        //    //var resultado = db.Personas.Where(c => c.Apellido == Apellido).FirstOrDefault();
        //    var resultado2 = false;
        //    if (resultado != null)
        //    {
        //        resultado2 = true;
        //    }
        //    return Json(resultado2, JsonRequestBehavior.AllowGet);
        //}

        //    protected override void Dispose(bool disposing)
        //    {
        //        if (disposing)
        //        {
        //            db.Dispose();
        //        }
        //        base.Dispose(disposing);
        //    }
    }
}
