using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ControlHouse.Models
{
    public class CuentaModel
    {
        public CuentaModel()
        {
            Movimientos = new List<MovimientosModel>();
            TransaccionesCuentaOrigen = new List<TransaccionModel>();
            TransaccionesCuentaDestino = new List<TransaccionModel>();
        }
        public int Id { get; set; }

        [StringLength(100)]
        [Required]
        public string Nombre { get; set; }
        public double Saldo { get; set; }

        public List<MovimientosModel> Movimientos { get; set; }
        public List<TransaccionModel> TransaccionesCuentaOrigen { get; set; }
        public List<TransaccionModel> TransaccionesCuentaDestino { get; set; }

        //public CuentaModel()
        //{
        //    Movimientos = new Collection<MovimientosModel>();
        //}
    }
}