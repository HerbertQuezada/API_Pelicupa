using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API_Pelicula.DTOs.Pelicula
{
    public class PeliculaIndexDTO
    {
        public List<PeliculaDTO> FuturoEstrenos { get; set; }
        public List<PeliculaDTO> EnCines { get; set; }
    }
}
