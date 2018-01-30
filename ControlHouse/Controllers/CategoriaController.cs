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
    public class CategoriaController : Controller
    {
        private CategoriasRepository db = new CategoriasRepository();

        public List<DropDownLists> GeneraMenu()
        {
            List<DropDownLists> lista = new List<DropDownLists>();
            lista.Add(new DropDownLists("Editar", "Edit", "Categoria"));
            lista.Add(new DropDownLists("Detalle", "Details", "Categoria"));
            lista.Add(new DropDownLists("Eliminar", "Delete", "Categoria"));
            //lista.Add(new DropDownLists("Sub Categoria", "Index", "SubCategoria"));
            return lista;
        }

        // GET: Categoria
        public ActionResult Index()
        {
            ViewBag.Menu = GeneraMenu();
            return View(db.Listar());
        }

        // GET: Categoria/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaModel categoriaModel = db.Devolver(id);
            if (categoriaModel == null)
            {
                return HttpNotFound();
            }
            return View(categoriaModel);
        }

        // GET: Categoria/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Categoria/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre")] CategoriaModel categoriaModel)
        {
            if (ModelState.IsValid)
            {
                db.Grabar(categoriaModel);
                return RedirectToAction("Index");
            }

            return View(categoriaModel);
        }

        // GET: Categoria/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaModel categoriaModel = db.Devolver(id);
            if (categoriaModel == null)
            {
                return HttpNotFound();
            }
            return View(categoriaModel);
        }

        // POST: Categoria/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre")] CategoriaModel categoriaModel)
        {
            if (ModelState.IsValid)
            {
                db.Actualizar(categoriaModel);
                return RedirectToAction("Index");
            }
            return View(categoriaModel);
        }

        // GET: Categoria/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CategoriaModel categoriaModel = db.Devolver(id);
            if (categoriaModel == null)
            {
                return HttpNotFound();
            }
            return View(categoriaModel);
        }

        // POST: Categoria/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            db.Eliminar(id);
            return RedirectToAction("Index");
        }       
    }
}
