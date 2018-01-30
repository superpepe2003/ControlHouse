using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ControlHouse.Models
{
    public class FiltroModel
    {
        public string Filtro { get; set; }
        public bool? Tipo { get; set; }
        [Display(Name="Fecha Inicial")]
        public DateTime? FechaInicial { get; set; }
        [Display(Name = "Fecha Final")]
        public DateTime? FechaFinal { get; set; }
    }
}