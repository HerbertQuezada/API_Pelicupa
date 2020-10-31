using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Validaciones
{
    public class TipoArchivoValidacion : ValidationAttribute
    {
        private readonly string[] tiposValidos;

        public TipoArchivoValidacion(GrupoTipoArchivo tipo)
        {
            if (tipo == GrupoTipoArchivo.Imagen) tiposValidos = new string[] { "image/png", "image/jpeg" };
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is null) return ValidationResult.Success;

            IFormFile formFile = (IFormFile)value;

            if (formFile is null) return ValidationResult.Success;

            if (!tiposValidos.Contains(formFile.ContentType)) return new ValidationResult($"Solo se aceptan archivo con formato png y jpg");

            return ValidationResult.Success;
        }

    }
}
