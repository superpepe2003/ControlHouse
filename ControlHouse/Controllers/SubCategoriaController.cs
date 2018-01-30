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
    public class SubCategoriaController : Controller
    {
        private SubCategoriasRepository db = new SubCategoriasRepository();
        private CategoriasRepository dc = new CategoriasRepository();
        private MovimientosRepository mov = new MovimientosRepository();

        public static int CategoriaId;

        public List<DropDownLists> GeneraMenu()
        {
            List<DropDownLists> lista = new List<DropDownLists>();
            lista.Add(new DropDownLists("Editar", "Edit", "SubCategoria"));
            lista.Add(new DropDownLists("Detalle", "Details", "SubCategoria"));
            lista.Add(new DropDownLists("Eliminar", "Delete", "SubCategoria"));
            return lista;
        }

        // GET: SubCategoria
        public ActionResult Index(int?id)
        {
            List<SubCategoriaModel> subCategoria;
            ViewBag.Menu = GeneraMenu();
            if (id.HasValue)
            {
                subCategoria = db.Listar(id.Value);
                ViewBag.DeCategoria = true;
                ViewBag.IdCategoria = id;
            }
            else
            {
                //var movimientos = mov.Listar();
                subCategoria = db.Listar();
                ViewBag.DeCategoria = false;
            }
            return View(subCategoria);
        }

        // GET: SubCategoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaModel subCategoriaModel = db.Devolver(id);
            if (subCategoriaModel == null)
            {
                return HttpNotFound();
            }
            return View(subCategoriaModel);
        }

        // GET: SubCategoria/Create
        public ActionResult Create(int? Id)
        {
            //SI ARRASTRA POR GET UN CODIGO DE CATEGORIA, LO CARGA Y DESHABILITA EL COMBO DE CATEGORIA PARA CARGAR SOLO SUB CATEGORIA A ESA CATEGORIA
            if (Id.HasValue)
            {
                ViewBag.TieneCategoria = true;
                ViewBag.CategoriaId = new SelectList(new List<CategoriaModel>() { dc.Devolver(Convert.ToInt32(Id)) }, "Id", "Nombre");
            }
            else
            {
                ViewBag.tieneCategoria = false;
                ViewBag.CategoriaId = new SelectList(dc.Listar(), "Id", "Nombre");
            }
            return View();
        }

        // POST: SubCategoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,CategoriaId, FijaMensualmente, Tipo, Monto")] SubCategoriaModel subCategoriaModel)
        {
            if (ModelState.IsValid)
            {
                db.Grabar(subCategoriaModel);
                return RedirectToAction("Index");
            }

            ViewBag.CategoriaId = new SelectList(dc.Listar(), "Id", "Nombre", subCategoriaModel.CategoriaId);
            return View(subCategoriaModel);
        }

        // GET: SubCategoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaModel subCategoriaModel = db.Devolver(id);
            if (subCategoriaModel == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoriaId = new SelectList(dc.Listar(), "Id", "Nombre", subCategoriaModel.CategoriaId);
            CategoriaId = subCategoriaModel.CategoriaId;
            return View(subCategoriaModel);
        }

        // POST: SubCategoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,CategoriaId, FijaMensualmente, Tipo, Monto")] SubCategoriaModel subCategoriaModel)
        {
            if (ModelState.IsValid)
            {                
                db.Actualizar(subCategoriaModel,CategoriaId);               
                return RedirectToAction("Index");
            }
            ViewBag.CategoriaId = new SelectList(dc.Listar(), "Id", "Nombre", subCategoriaModel.CategoriaId);            
            return View(subCategoriaModel);
        }

        // GET: SubCategoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SubCategoriaModel subCategoriaModel = db.Devolver(id);
            if (subCategoriaModel == null)
            {
                return HttpNotFound();
            }
            return View(subCategoriaModel);
        }

        // POST: SubCategoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Eliminar(id);
            return RedirectToAction("Index");
        }

    }
}
























































