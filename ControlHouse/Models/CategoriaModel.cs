using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ControlHouse.Models
{
    public class CategoriaModel
    {
        public CategoriaModel()
        {
            SubCategorias = new List<SubCategoriaModel>();
            Movimientos = new List<MovimientosModel>();
        }

        public int Id { get; set; }

        [DisplayName("Categoria")]
        public string Nombre { get; set; }


        public List<SubCategoriaModel> SubCategorias { get; set; }

        public List<MovimientosModel>Movimientos { get; set; }

        //public CategoriaModel()
        //{
        //    SubCategorias = new Collection<SubCategoriaModel>();
        //    Movimientos = new Collection<MovimientosModel>();
        //}
    }
}