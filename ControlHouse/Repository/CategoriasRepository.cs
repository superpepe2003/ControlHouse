using ControlHouse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlHouse.Repository
{
    public class CategoriasRepository : IRepository<CategoriaModel>
    {
        public void Actualizar(CategoriaModel datos)
        {
            using (var c = new Contexto())
            {
                c.Entry(datos).State = EntityState.Modified;
                c.SaveChanges();
            }
        }

        public CategoriaModel Devolver(int? id)
        {
            using (var c = new Contexto())
            {
                return c.Categoria.Where(xc => xc.Id == id)
                    .Include(xc => xc.SubCategorias)
                    .SingleOrDefault();
            }
        }

        public void Eliminar(int id)
        {
            using (var c = new Contexto())
            {
                c.Entry(new CategoriaModel { Id = id })
                    .State = EntityState.Deleted;
                c.SaveChanges();
            }
        }

        public void Grabar(CategoriaModel datos)
        {
            using (var c = new Contexto())
            {
                c.Categoria.Add(datos);
                c.SaveChanges();

            }
        }

        public List<CategoriaModel> Listar()
        {
            using (var c = new Contexto())
            {
                return c.Categoria.Include(x => x.SubCategorias).ToList();
            }
        }
    }
}