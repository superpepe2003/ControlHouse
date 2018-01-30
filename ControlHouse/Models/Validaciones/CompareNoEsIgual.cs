using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ControlHouse.Models.Validaciones
{
    public class CompareNoEsIgual : ValidationAttribute //, IClientValidatable
    {
        private string _otra;
        public CompareNoEsIgual(string otra)
        {
            _otra = otra;
        }

        //public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        //{

        //    var modelClientValidationRule = new ModelClientValidationRule
        //    {
        //        ErrorMessage = FormatErrorMessage(metadata.GetDisplayName())
        //    };
        //    modelClientValidationRule.ValidationParameters.Add("param",
        //        _otra);
        //    modelClientValidationRule.ValidationType = "comparenoesigual";
        //    yield return modelClientValidationRule;
        //}

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var otraProperty = validationContext.ObjectType.GetProperty(_otra);
            var otraParamValue = otraProperty.GetValue(validationContext.ObjectInstance, null);
            return ((int)otraParamValue != (int) value) ? null : new ValidationResult
                ("Las Cuentas Origen y Destino no pueden ser iguales SERVIDOR");
        }
    }

    //public class RemoteMontoInferior: RemoteAttribute
    //{
    //    private string _campo;

    //    public RemoteMontoInferior(string Campo)
    //    {
    //        _campo = Campo;
    //    }

    //    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    //    {
    //        var otraProperty = validationContext.ObjectType.GetProperty(_campo);
    //        var otraParamValue = otraProperty.GetValue(validationContext.ObjectInstance, null);
    //        return ((int)otraParamValue != (int)value) ? null : new ValidationResult
    //            ("Las Cuentas Origen y Destino no pueden ser iguales SERVIDOR");
    //    }
    //}
}