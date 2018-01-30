using ControlHouse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlHouse.Repository
{
    public class SubCategoriasRepository
    {
        public void Actualizar(SubCategoriaModel datos, int idCategoriaViejo)
        {
            using (var c = new Contexto())
            {
                if(datos.CategoriaId!=idCategoriaViejo)
                {
                    //Modifico los movimientos idCategoria
                    List<MovimientosModel> mov = c.Movimiento.Where(f => f.SubCategoriaId == datos.Id).ToList();
                    foreach (MovimientosModel m in mov)
                        m.CategoriaId = datos.CategoriaId;
                }
                c.Entry(datos).State = EntityState.Modified;
                c.SaveChanges();
            }
        }

        public SubCategoriaModel Devolver(int? id)
        {
            using (var c = new Contexto())
            {
                return c.SubCategoria
                        .Where(m=>m.Id==id)
                        .Include(m => m.Categoria)
                        .SingleOrDefault();
            }
        }

        public void Eliminar(int id)
        {
            using (var c = new Contexto())
            {
                c.Entry(new SubCategoriaModel { Id = id }).State = EntityState.Deleted;
                c.SaveChanges();
            }
        }

        public void Grabar(SubCategoriaModel datos)
        {
            using (var c = new Contexto())
            {
                c.SubCategoria.Add(datos);
                c.SaveChanges();
            }
        }

        public List<SubCategoriaModel> Listar(int id)
        {
            using (var c = new Contexto())
            {
                return c.SubCategoria
                        .Where(m=>m.CategoriaId==id)
                        .Include(m => m.Categoria)
                        .ToList();
            }
        }

        public List<SubCategoriaModel> Listar()
        {
            using (var c = new Contexto())
            {
                return c.SubCategoria
                        .Include(m=>m.Categoria)
                        .ToList();
            }
        }

        public List<SubCategoriaModel> ListarFijasIngreso()
        {
            using (var c = new Contexto())
            {
                return c.SubCategoria
                        .Where(e => (e.FijaMensualmente == true) && (e.Tipo==true))
                        .Include(m => m.Categoria)
                        .ToList();
            }
        }

        public List<SubCategoriaModel> ListarFijasEgreso()
        {
            using (var c = new Contexto())
            {
                return c.SubCategoria
                        .Where(e => (e.FijaMensualmente == true) && (e.Tipo == false))
                        .Include(m => m.Categoria)
                        .ToList();
            }
        }       
    }
}