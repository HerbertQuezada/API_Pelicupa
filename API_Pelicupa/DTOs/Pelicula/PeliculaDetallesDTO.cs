using API_Pelicula.DTOs.Genero;
using API_Pelicula.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Pelicula
{
    public class PeliculaDetallesDTO: Entidades.Pelicula
    { 
        public List<ActorPeliculaDetalleDTO> PeliculaActores { get; set; }
        public List<GeneroDTO> PeliculaGeneros { get; set; }
    }
}
