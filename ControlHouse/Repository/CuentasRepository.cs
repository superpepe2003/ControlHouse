using ControlHouse.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace ControlHouse.Repository
{
    public class CuentasRepository
    {
        public void Actualizar(CuentaModel datos)
        {
            using (var c = new Contexto())
            {
                c.Entry(datos).State = EntityState.Modified;                
                c.SaveChanges();
            }
        }

        public CuentaModel Devolver(int? id)
        {
            using (var c = new Contexto())
            {
                return c.Cuenta
                    .Where(x => x.Id == id)
                    .Include(x=>x.Movimientos)
                    .SingleOrDefault();                        
            }
        }

        public void Eliminar(int id)
        {
            using (var c = new Contexto())
            {
                c.Entry(new CuentaModel { Id = id })
                    .State = EntityState.Deleted;
                c.SaveChanges();
            }
        }

        public void Grabar(CuentaModel datos)
        {
            using (var c = new Contexto())
            {
                c.Cuenta.Add(datos);
                c.SaveChanges();
            }
        }

        public List<CuentaModel> Listar()
        {
            using (var c = new Contexto())
            {
                return c.Cuenta.Include(x=>x.Movimientos).ToList();
            }
        }

        public void MovimientosCuenta(CuentaModel CuentaDescontar, CuentaModel CuentaSumar, double monto)
        {
            if(CuentaDescontar != null)
            {
                CuentaDescontar.Saldo -= monto;           
            }
            if(CuentaSumar != null)
            {
                CuentaSumar.Saldo += monto;
            }
            
        }

        public double CalcularTotal()
        {
            List<CuentaModel> Cuentas = Listar();
            
            double total = Cuentas.Sum(c => c.Saldo); 
            //foreach (var c in Cuentas)
            //    total += c.Saldo;
            return total;
        }
    }
}