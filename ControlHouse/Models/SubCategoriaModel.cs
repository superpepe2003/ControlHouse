
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ControlHouse.Models
{
    public class SubCategoriaModel
    {
        public SubCategoriaModel()
        {
            Movimientos = new List<MovimientosModel>();
        }

        public int Id { get; set; }

        [DisplayName("Sub Categoria")]
        public string Nombre { get; set; }        
        public CategoriaModel Categoria { get; set; }

        [DisplayName("Fija Mensual")]
        public bool FijaMensualmente { get; set; }

        public double Monto { get; set; }

        public bool Tipo { get; set; }

        [DisplayName("Categoria")]
        public int CategoriaId { get; set; }

        public List<MovimientosModel> Movimientos { get; set; }

        //public SubCategoriaModel()
        //{
        //    Categoria = new CategoriaModel();
        //    Movimientos = new Collection<MovimientosModel>();
        //}
    }
}