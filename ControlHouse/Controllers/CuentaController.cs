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

namespace ControlHouse.Controllers
{
    public class CuentaController : Controller
    {
        private CuentasRepository _repository = new CuentasRepository();

        public List<DropDownLists> GeneraMenu()
        {
            List<DropDownLists> lista = new List<DropDownLists>();
            lista.Add(new DropDownLists("Editar", "Edit", "Cuenta"));
            lista.Add(new DropDownLists("Detalle", "Details", "Cuenta"));
            lista.Add(new DropDownLists("Eliminar", "Delete", "Cuenta"));
            return lista;
        }

        // GET: Cuenta
        public ActionResult Index()
        {
            ViewBag.Total = _repository.CalcularTotal();
            ViewBag.Menu = GeneraMenu();
            return View(_repository.Listar());
        }

        // GET: Cuenta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaModel cuentaModel = _repository.Devolver(id);
            if (cuentaModel == null)
            {
                return HttpNotFound();
            }
            return View(cuentaModel);
        }

        // GET: Cuenta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cuenta/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Saldo")] CuentaModel cuentaModel)
        {
            if (ModelState.IsValid)
            {
                _repository.Grabar(cuentaModel);
                return RedirectToAction("Index");
            }

            return View(cuentaModel);
        }       

        // GET: Cuenta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaModel cuentaModel = _repository.Devolver(id);
            if (cuentaModel == null)
            {
                return HttpNotFound();
            }
            return View(cuentaModel);
        }

        // POST: Cuenta/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Saldo")] CuentaModel cuentaModel)
        {
            if (ModelState.IsValid)
            {
                _repository.Actualizar(cuentaModel);
                return RedirectToAction("Index");
            }
            return View(cuentaModel);
        }

        // GET: Cuenta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CuentaModel cuentaModel = _repository.Devolver(id);
            if (cuentaModel == null)
            {
                return HttpNotFound();
            }
            return View(cuentaModel);
        }

        // POST: Cuenta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            _repository.Eliminar(id);
            return RedirectToAction("Index");
        }

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        _repository.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}
    }
}
