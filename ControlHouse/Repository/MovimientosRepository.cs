using ControlHouse.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace ControlHouse.Repository
{
    public class MovimientosRepository
    {
        public void Actualizar(MovimientosModel datos, double MontoAnterior, int CuentaAnteriorId, bool TipoAnterior)
        {
            using (var c = new Contexto())
            { 
                CuentasRepository RepoCuentas = new CuentasRepository();
                CuentaModel _cuentaAnterior = RepoCuentas.Devolver(CuentaAnteriorId);
                CuentaModel _cuentaActual;
                if (CuentaAnteriorId == datos.CuentaId)
                    _cuentaActual = _cuentaAnterior;
                else
                    _cuentaActual = RepoCuentas.Devolver(datos.CuentaId);

                //if (TipoAnterior)
                //{
                //    RepoCuentas.MovimientosCuenta(_cuentaAnterior, null, MontoAnterior);
                //    if (!datos.Tipo.Value)
                //        RepoCuentas.MovimientosCuenta(_cuentaActual, null, datos.Monto);
                //    else
                //        RepoCuentas.MovimientosCuenta(null, _cuentaActual, datos.Monto);
                //}
                //else
                //{
                //    RepoCuentas.MovimientosCuenta(null, _cuentaAnterior, MontoAnterior);
                //    if (!datos.Tipo.Value)
                //        RepoCuentas.MovimientosCuenta(_cuentaActual, null, datos.Monto);
                //    else
                //        RepoCuentas.MovimientosCuenta(null, _cuentaActual, datos.Monto);
                //}
               
                if ((TipoAnterior != datos.Tipo) || (CuentaAnteriorId != datos.CuentaId) || (MontoAnterior != datos.Monto))
                {
                    if(datos.CuentaId==CuentaAnteriorId)
                    if (TipoAnterior)
                        RepoCuentas.MovimientosCuenta(_cuentaAnterior, null, MontoAnterior);
                    else
                        RepoCuentas.MovimientosCuenta(null, _cuentaAnterior, MontoAnterior);

                    if (datos.Tipo.Value)
                        RepoCuentas.MovimientosCuenta(null, _cuentaActual, datos.Monto);
                    else
                        RepoCuentas.MovimientosCuenta(_cuentaActual, null, datos.Monto);
                }

                c.Entry(datos).State = EntityState.Modified;

                if (_cuentaActual.Id == _cuentaAnterior.Id)
                {
                    _cuentaActual.Movimientos = null;
                    c.Entry(_cuentaActual).State = EntityState.Modified;
                }
                else
                {
                    _cuentaActual.Movimientos = null;
                    _cuentaAnterior.Movimientos = null;
                    c.Entry(_cuentaActual).State = EntityState.Modified;
                    c.Entry(_cuentaAnterior).State = EntityState.Modified;
                }                

                c.SaveChanges();
            }
        }


        //DifMonto hace el calculo entre el monto anterior descontandole el nuevo monto y esa diferencia si es positiva le devuelve a la cuenta y si no le resta;
        //public void Actualizar(MovimientosModel datos,double DifMonto)
        //{
        //    using (var c = new Contexto())
        //    {
        //            CuentasRepository RepoCuentas = new CuentasRepository();
        //            //CONTROLO SI ES UN INGRESO O EGRESO
        //            if (!datos.Tipo.Value)
        //            {
        //                if (DifMonto > 0)
        //                    RepoCuentas.MovimientosCuenta(-1, datos.CuentaId, DifMonto);
        //                else
        //                    RepoCuentas.MovimientosCuenta(datos.CuentaId, -1, DifMonto);
                        
        //            }
        //            else
        //            {                        
        //                if (DifMonto > 0)
        //                    RepoCuentas.MovimientosCuenta(datos.CuentaId,-1, DifMonto);
        //                else
        //                    RepoCuentas.MovimientosCuenta(-1, datos.CuentaId, DifMonto);                        
        //            }
        //            c.Entry(datos).State = EntityState.Modified;
        //            c.SaveChanges();
        //    }
        //}

        //en esta recarga paso el monto anterior si haber hecho la diferencia por que aca se revierte ese monto si era un ingreso y se pone el nuevo como egreso, o al revez
        //public void Actualizar(MovimientosModel datos, double MontoAnterior, bool f)
        //{
        //    if (f)
        //    {
        //        using (var c = new Contexto())
        //        {
        //                CuentasRepository RepoCuentas = new CuentasRepository();
        //                //SI ES UN EGRESO AJUSTO EL MONTO Y SI ES UN INGRESO LO HAGO INVERSO, TENGO EN CUENTA QUE TNEGO QUE REVERTIR LA OPERACION ANTERIOR
        //                if (!datos.Tipo.Value)
        //                {                                                        
        //                    RepoCuentas.MovimientosCuenta(datos.CuentaId, -1, MontoAnterior);                            
        //                    RepoCuentas.MovimientosCuenta(datos.CuentaId, -1, datos.Monto);
        //                }
        //                else
        //                {
        //                    RepoCuentas.MovimientosCuenta(-1,datos.CuentaId, MontoAnterior);
        //                    RepoCuentas.MovimientosCuenta(-1,datos.CuentaId, datos.Monto);
        //                }
        //                c.Entry(datos).State = EntityState.Modified;
        //                c.SaveChanges();
        //        }
        //    }
        //}

        public MovimientosModel Devolver(int? id)
        {
            using (var c = new Contexto())
            {
                return c.Movimiento.Where(m => m.Id == id)
                    .Include(m => m.Cuenta)
                    .Include(m => m.Categoria)
                    .Include(m => m.SubCategoria)
                    .SingleOrDefault();
            }
        }

        public void Eliminar(MovimientosModel datos)
        {
            using (var c = new Contexto())
            {
                CuentasRepository RepoCuentas = new CuentasRepository();
                CuentaModel _cuenta = RepoCuentas.Devolver(datos.CuentaId);
                RepoCuentas.MovimientosCuenta(null, _cuenta, datos.Monto);
                _cuenta.Movimientos = null;
                datos.Cuenta = null;
                c.Entry(_cuenta).State = EntityState.Modified;
                c.Entry(datos).State = EntityState.Deleted;
                c.SaveChanges();
                    
            }
        }

        public void Grabar(MovimientosModel datos)
        {
            using (var c = new Contexto())
            {
                CuentasRepository RepoCuentas = new CuentasRepository();
                CuentaModel _cuenta = RepoCuentas.Devolver(datos.CuentaId);
                if (!datos.Tipo.Value)
                {
                    RepoCuentas.MovimientosCuenta(_cuenta, null, datos.Monto);
                }
                else
                    RepoCuentas.MovimientosCuenta(null, _cuenta, datos.Monto);
                c.Movimiento.Add(datos);
                c.Entry(_cuenta).State = EntityState.Modified;
                c.SaveChanges();

            }
        }

        public List<MovimientosModel> Listar()
        {
            using (var c = new Contexto())
            {
                List<MovimientosModel> movs = c.Movimiento.Include(m=>m.Cuenta).Include(m=>m.Categoria).Include(m=>m.SubCategoria).ToList();
                return movs;                 
            }
        }

        public int ContarMovimientos(FiltroModel filtro)
        {
            using (var c = new Contexto())
            {
                IQueryable<MovimientosModel> query = c.Movimiento;
                query = ListarFiltro(filtro, query);
                return query.Count();
            }
        }

        public IQueryable<MovimientosModel> ListarFiltro(FiltroModel filtro, IQueryable<MovimientosModel> query)
        {
            if (filtro.FechaInicial != null)
                query = query.Where(c => c.Fecha >= filtro.FechaInicial && c.Fecha <= filtro.FechaFinal);
            if (filtro.Tipo != null)
                query = query.Where(c => c.Tipo == filtro.Tipo);
            if ((filtro.Filtro != "")&&(filtro.Filtro!=null))
                query = query.Where(c => c.Categoria.Nombre.Contains(filtro.Filtro) ||
                                       c.SubCategoria.Nombre.Contains(filtro.Filtro) ||
                                       c.Cuenta.Nombre.Contains(filtro.Filtro) ||
                                       c.Monto.ToString().Contains(filtro.Filtro) ||
                                       c.HashTag.ToString().Contains(filtro.Filtro) ||
                                       c.Descripcion.Contains(filtro.Filtro));
            return query;                                    
        }

        public IDictionary<string, double> ListarEgresoIngreso(FiltroModel filtro)
        {
            IDictionary<string,double> valores = new Dictionary<string, double>();
            using (var j = new Contexto())
            {
                IQueryable<MovimientosModel> query = j.Movimiento;
                query = ListarFiltro(filtro, query);

                List<MovimientosModel> query2 = query.ToList();
                valores.Add("ingreso",query2.Where(c => c.Tipo == true).Sum(s => s.Monto));
                valores.Add("egreso", query2.Where(c => c.Tipo == false).Sum(s => s.Monto));
            }
            return valores;
        }

        public IEnumerable<MovimientosModel> Listar<T>(FiltroModel filtro, int paginaActual, int personasPorPagina, Expression<Func<MovimientosModel,T>> ordenacion, Direccion direccion)
        {
            using (var c = new Contexto())
            {
                if (paginaActual < 1) paginaActual = 1;
                IQueryable<MovimientosModel> query = c.Movimiento;
                query = ListarFiltro(filtro, query);
                if (direccion == Direccion.Ascendente)
                    query = query.OrderBy(ordenacion);
                else
                    query = query.OrderByDescending(ordenacion);
                return query.Skip((paginaActual - 1) * personasPorPagina)
                            .Take(personasPorPagina)
                            .Include(m => m.Cuenta)
                            .Include(m => m.Categoria)
                            .Include(m => m.SubCategoria)
                            .ToList();

            }
                
        }
    }
}