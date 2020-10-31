using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.Validaciones
{
    public class PesoArchivoValidacion : ValidationAttribute
    {
        private readonly int PesoMaximoMB;

        public PesoArchivoValidacion(int PesoMaximoMB)
        {
            this.PesoMaximoMB = PesoMaximoMB;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null) return ValidationResult.Success;

            IFormFile formFile = value as IFormFile; 
            if (formFile == null) return ValidationResult.Success;

            if (formFile.Length > PesoMaximoMB * 1024 * 1024) return new ValidationResult($"El peso del archivo no puede ser mayor a {PesoMaximoMB} MB");

            return ValidationResult.Success;
        }
    }
}
