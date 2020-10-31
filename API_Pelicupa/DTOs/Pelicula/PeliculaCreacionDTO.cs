using API_Pelicula.Helpers;
using API_Pelicula.Validaciones;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Pelicula
{
    public class PeliculaCreacionDTO
    { 
        [Required]
        [StringLength(100)]
        public string Titulo { get; set; }

        public bool EnCines { get; set; }

        public DateTime FechaEstreno { get; set; }

        [PesoArchivoValidacion(PesoMaximoMB: 4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.Imagen)]
        public IFormFile Poster { get; set; } 

        [ModelBinder(BinderType = typeof(TypeBinder<List<int>>))]
        public List<int> GeneroIDs { get; set; }

        [ModelBinder(BinderType = typeof(TypeBinder<List<ActorPeliculaCreacionDTO>>))]
        public List<ActorPeliculaCreacionDTO> Actor { get; set; }
    }
}
