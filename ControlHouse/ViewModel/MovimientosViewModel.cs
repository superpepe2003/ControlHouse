using ControlHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ControlHouse.ViewModel
{
    public class MovimientosViewModel
    {
        public IEnumerable<MovimientosModel> Movimientos;
        public FiltroModel filtro;
        public int NumeroDePersonas;
        public int PersonasPorPagina;
        public IDictionary<string, double> valores;

        public MovimientosViewModel()
        {
            Movimientos = new List<MovimientosModel>();
            filtro = new FiltroModel();
            valores = new Dictionary<string, double>();
        }
     }
}