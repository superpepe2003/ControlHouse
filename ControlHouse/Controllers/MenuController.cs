using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlHouse.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu
        public ActionResult Inicio()
        {
            ViewBag.Movimiento = new Models.MovimientosModel();
            return View();
        }
    }
}