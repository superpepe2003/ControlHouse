using ControlHouse.Models.Validaciones;
using ControlHouse.Repository;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlHouse.Models
{
    public class TransaccionModel//:IValidatableObject
    {        
        public int Id { get; set; }

        public DateTime Fecha { get; set; }


        [Display(Name = "Cuenta de Origen")]
        public int CuentaIdOrigen { get; set; }
        public CuentaModel CuentaOrigen { get; set; }
              


        [Display(Name = "Cuenta Destino")]
        [CompareNoEsIgual("CuentaIdOrigen")]
        public int CuentaIdDestino { get; set; }
        public CuentaModel CuentaDestino { get; set; }

        
        [Required]
        [Remote("HayMonto", "Transaccion",null,AdditionalFields = "Id,CuentaIdOrigen",ErrorMessage ="No hay monto suficiente REMOTO")]
        public double Monto { get; set; }

        public string Descripcion { get; set; }

        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    var errores = new List<ValidationResult>();

        //    if (CuentaIdOrigen == CuentaIdDestino)
        //    {
        //        errores.Add(new ValidationResult("La cuenta destino no puede ser la misma que la cuenta origen"));//, new string[] { "CuentaIdDestino" }));
        //    }
        //    CuentasRepository _repo = new CuentasRepository();
        //    if (_repo.Devolver(CuentaIdOrigen).Saldo<Monto)
        //    {
        //        errores.Add(new ValidationResult("La cuenta origen no tiene suficiente monto"));
        //    }
        //    return errores;
        //}
    }
}