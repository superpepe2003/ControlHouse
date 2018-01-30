using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ControlHouse.Models;

namespace ControlHouse.ViewModel
{
    public class TransaccionesViewModel
    {
        public IEnumerable<TransaccionModel> Transaccion;
        public FiltroModel filtro;
        public int NumeroDePersonas;
        public int PersonasPorPagina;

        public TransaccionesViewModel()
        {
            Transaccion = new List<TransaccionModel>();
            filtro = new FiltroModel();
        }
    }
}