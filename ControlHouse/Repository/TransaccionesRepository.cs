using ControlHouse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ControlHouse.Repository
{
    public class TransaccionesRepository
    {
        public void Actualizar(TransaccionModel datos)
        {
            using (var c = new Contexto())
            {
                c.Entry(datos).State = EntityState.Modified;
                c.SaveChanges();
            }
        }

        public TransaccionModel Devolver(int? id)
        {
            using (var c = new Contexto())
            {
                return c.Transaccion.Where(x => x.Id == id)
                    .Include(x=>x.CuentaOrigen)
                    .Include(x=>x.CuentaDestino)
                    .SingleOrDefault();
            }
        }

        public void Eliminar(TransaccionModel datos)
        {
            using (var c = new Contexto())
            {
                CuentasRepository cr = new CuentasRepository();
                CuentaModel _cuentaOrigen = cr.Devolver(datos.CuentaIdOrigen);
                CuentaModel _cuentaDestino = cr.Devolver(datos.CuentaIdDestino);
                cr.MovimientosCuenta(_cuentaDestino, _cuentaOrigen, datos.Monto);
                _cuentaOrigen.TransaccionesCuentaOrigen = null;
                _cuentaOrigen.TransaccionesCuentaDestino = null;
                _cuentaDestino.TransaccionesCuentaOrigen = null;
                _cuentaDestino.TransaccionesCuentaDestino = null;

                c.Entry(_cuentaOrigen).State = EntityState.Modified;
                c.Entry(_cuentaDestino).State = EntityState.Modified;
                c.Entry(datos).State = EntityState.Deleted;
                c.SaveChanges();
                

            }
        }

        //le paso el modelo a actualizar o agregar y el monto que es la diferencia a actualizar en el caso que haya cambiado el monto, 
        //lo hago todo en una transaccion para que aplique todos los cambios
        public void Grabar(TransaccionModel datos, double monto)
        {
            using (var c = new Contexto())
            {
                CuentasRepository cr = new CuentasRepository();
                CuentaModel _cuentaOrigen = cr.Devolver(datos.CuentaIdOrigen);
                CuentaModel _cuentaDestino = cr.Devolver(datos.CuentaIdDestino);
                if (datos.Id > 0)
                {
                    c.Entry(datos).State = EntityState.Modified;                        
                    //Si monto es mayor que 0 tiene que devolver a la cuentaOrigen y restarle a la destino
                    if (monto > 0)
                    {                            
                        cr.MovimientosCuenta(_cuentaDestino, _cuentaOrigen, monto);
                    }
                    //Caso contrario tiene que sumarle la diferencia que es el Monto a cuentaOrigen y restarle a Destino
                    else
                    {
                        cr.MovimientosCuenta(_cuentaOrigen, _cuentaDestino, monto);
                    }
                    _cuentaOrigen.TransaccionesCuentaOrigen = null;
                    _cuentaOrigen.TransaccionesCuentaDestino = null;
                    _cuentaDestino.TransaccionesCuentaOrigen = null;
                    _cuentaDestino.TransaccionesCuentaDestino = null;
                }
                else
                {                    
                    c.Transaccion.Add(datos);
                    cr.MovimientosCuenta(_cuentaOrigen, _cuentaDestino, datos.Monto);
                }
                
                c.Entry(_cuentaOrigen).State = EntityState.Modified;
                c.Entry(_cuentaDestino).State = EntityState.Modified;
                c.SaveChanges();               
            }
        }

        public List<TransaccionModel> Listar()
        {
            using (var c = new Contexto())
            {
                return c.Transaccion
                    .Include(x => x.CuentaOrigen)
                    .Include(x => x.CuentaDestino)
                    .ToList();
            }
        }

        public int ContarTransacciones(FiltroModel filtro)
        {
            using (var c = new Contexto())
            {
                IQueryable<TransaccionModel> query = c.Transaccion;
                query = ListarFiltro(filtro,query);
                return query.Count();
            }
        }

        public IQueryable<TransaccionModel> ListarFiltro(FiltroModel filtro, IQueryable<TransaccionModel> query)
        {
            if (filtro.FechaInicial != null)
                query = query.Where(c => c.Fecha >= filtro.FechaInicial && c.Fecha <= filtro.FechaFinal);
            if ((filtro.Filtro != "") && (filtro.Filtro != null))
                query = query.Where(c => c.CuentaDestino.Nombre.Contains(filtro.Filtro) ||
                                       c.CuentaOrigen.Nombre.Contains(filtro.Filtro) ||
                                       c.Monto.ToString().Contains(filtro.Filtro) ||
                                       c.Descripcion.Contains(filtro.Filtro));
            return query;
        }


        public IEnumerable<TransaccionModel> Listar<T>(FiltroModel filtro, int paginaActual, int personasPorPagina, Expression<Func<TransaccionModel, T>> ordenacion, Direccion direccion)
        {
            using (var c = new Contexto())
            {
                if (paginaActual < 1) paginaActual = 1;
                IQueryable<TransaccionModel> query = c.Transaccion;
                query = ListarFiltro(filtro, query);
                if (direccion == Direccion.Ascendente)
                    query = query.OrderBy(ordenacion);
                else
                    query = query.OrderByDescending(ordenacion);
                return query.Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .Include(m => m.CuentaOrigen)
                            .Include(m => m.CuentaDestino)
                            .ToList();

            }

        }


    }
}