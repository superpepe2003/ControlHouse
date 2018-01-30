using ControlHouse.Models;
using ControlHouse.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlHouse.Controllers
{

    public class _cantidad
    {
        public int Ingresos { get; set; }
        public int Egresos { get; set; }
    }

    public class BalancaViewModel
    {
        public List<string> Meses { get; set; }
        public List<SubCategoriaModel> SubCategoriaIngreso { get; set; }
        public List<SubCategoriaModel> SubCategoriaEgreso { get; set; }

        public _cantidad Cantidad { get; set; }   
        public double MontoTotal { get; set; }    
        
        public BalancaViewModel()
        {
            Meses = new List<string>();
            SubCategoriaIngreso = new List<SubCategoriaModel>();
            SubCategoriaEgreso = new List<SubCategoriaModel>();
            Cantidad = new _cantidad();
        }
    }    

    public class BalanceController : Controller
    {
        public SubCategoriasRepository _repoSubCategoria = new SubCategoriasRepository();
        public CuentasRepository _repoCuentas = new CuentasRepository();
        // GET: Balance
        public ActionResult Index()
        {
            BalancaViewModel balance = new BalancaViewModel { Meses = CalcularMes(),
                                                              SubCategoriaIngreso = _repoSubCategoria.ListarFijasIngreso(),
                                                              SubCategoriaEgreso = _repoSubCategoria.ListarFijasEgreso(),
                                                              };
            balance.Cantidad.Egresos = balance.SubCategoriaEgreso.Count();
            balance.Cantidad.Ingresos = balance.SubCategoriaIngreso.Count();
            balance.MontoTotal = _repoCuentas.CalcularTotal();
            return View(balance);
        }

        public List<string> CalcularMes()
        {            
            List<string> Meses = new List<string>();
            string mesActual = DateTime.Now.ToString("MMMM") ;
            int numMesActual = DateTime.Now.Month;
            //int f = 13 - numMesActual;

            //if(numMesActual==12)
            //{
            //    f = 13;            
            //}

            for(int i=0;i<12;i++)
            {
                Meses.Add(DateTime.Now.AddMonths(i).ToString("MMMM"));
            }

            return Meses;
        }
    }
}