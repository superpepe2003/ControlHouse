using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlHouse.Models
{
    public class MovimientosModel
    {
        //public MovimientosModel()
        //{
        //    Categoria = new CategoriaModel();
        //    SubCategoria = new SubCategoriaModel();
        //    Cuenta = new CuentaModel();
        //}

        public int Id { get; set; }
        [Required]
        public DateTime Fecha { get; set; }

        
        public CategoriaModel Categoria { get; set; }
        [Display(Name="Categoria")]
        [Required]
        public int CategoriaId { get; set; }

        
        public SubCategoriaModel SubCategoria { get; set; }
        [Display(Name = "Sub Categoria")]
        [Required]
        public int SubCategoriaId { get; set; }

        
        public CuentaModel Cuenta { get; set; }
        [Display(Name = "Cuenta")]
        [Required]
        [Remote("TieneMonto", "Movimientos", AdditionalFields = "Id,CuentaId, Tipo", ErrorMessage = "No hay monto en la Cuenta")]
        public int CuentaId { get; set;}

        [Required]
        //[Remote("TieneMonto", "Movimientos", AdditionalFields = "Id,CuentaId,Tipo",ErrorMessage = "No Hay monto suficiente REMOTO MOVIMIENTOS")]
        [Remote("TieneMonto", "Movimientos", AdditionalFields = "Id,CuentaId, Tipo", ErrorMessage = "No hay monto en la Cuenta")]
        public double Monto { get; set; }

        [DataType(DataType.MultilineText)]
        public string HashTag { get; set; }

        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set; }

        //1 es un ingreso 0 es un egreso
        public bool? Tipo { get; set; }

        
    }
}