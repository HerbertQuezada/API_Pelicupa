using API_Pelicula.Validaciones;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Actor
{
    public class ActorCrearDTO
    { 
        [Required]
        [StringLength(80)]
        public string Nombre { get; set; }
        public DateTime FechaNacimiento { get; set; }
        [PesoArchivoValidacion(PesoMaximoMB: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        public IFormFile Foto { get; set; }
    }
}
